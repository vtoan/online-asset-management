using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class AssetRepository : BaseRepository, IAssetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AssetRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest)
        {
            await this.LocationIsExist(_dbContext, assetRequest.LocationId);
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == assetRequest.CategoryId);
            if (category == null)
            {
                throw new Exception("Repository | Have not this category");
            }
            assetRequest.AssetId = category.ShortName + (100000 + category.NumIncrease + 1).ToString();
            var asset = new Asset
            {
                AssetId = assetRequest.AssetId,
                CategoryId = assetRequest.CategoryId,
                AssetName = assetRequest.AssetName,
                Specification = assetRequest.Specification,
                InstalledDate = assetRequest.InstalledDate,
                State = assetRequest.State,
                LocationId = assetRequest.LocationId
            };

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var create = _dbContext.Assets.Add(asset);
                category.NumIncrease = category.NumIncrease + 1;
                var result = await _dbContext.SaveChangesAsync();
                transaction.Commit();
                if (result > 0)
                {
                    var assetmodel = new AssetModel
                    {
                        AssetId = create.Entity.AssetId,
                        AssetName = create.Entity.AssetName,
                        CategoryName = create.Entity.Category.CategoryName,
                        State = create.Entity.State
                    };
                    return assetmodel;
                }
            }
            catch
            {
                throw new Exception("Repository | Create Asset Fail");
            }
            throw new Exception("Repository | Create Asset Fail");
        }
        public async Task<bool> DeleteAssetAsync(string id)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssetId == id);
            if (assignment != null)
            {
                throw new Exception("Repository | This asset have a assignment");
            }
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == id);
            if (asset == null)
            {
                throw new Exception("Repository | Have not this asset");
            }
            var deleted = _dbContext.Assets.Remove(asset);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            throw new Exception("Repository | Delete asset fail");
        }
        public async Task<AssetDetailModel> GetAssetByIdAsync(string id)
        {
            var asset = await _dbContext.Assets.Include(x => x.Category).Include(x => x.Location).FirstOrDefaultAsync(x => x.AssetId == id);
            if (asset == null)
            {
                throw new Exception("Repository | Have not this asset");
            }
            var assetmodel = new AssetDetailModel
            {
                AssetId = asset.AssetId,
                AssetName = asset.AssetName,
                CategoryName = asset.Category.CategoryName,
                CategoryId = asset.CategoryId,
                LocationId = asset.LocationId,
                LocationName = asset.Location.LocationName,
                Specification = asset.Specification,
                InstalledDate = asset.InstalledDate.Value,
                State = asset.State
            };
            return assetmodel;
        }
        public async Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(AssetRequestParams assetRequestParams)
        {
            await this.LocationIsExist(_dbContext, assetRequestParams.LocationId);
            //filter
            var queryable = _dbContext.Assets.Include(x => x.Category).AsQueryable();
            queryable = queryable.Where(x => x.LocationId == assetRequestParams.LocationId);
            if (assetRequestParams.State != null)
            {
                var stateNum = Array.ConvertAll(assetRequestParams.State, value => (int)value);
                queryable = queryable.Where(x => stateNum.Contains(x.State));
            }
            if (assetRequestParams.CategoryId != null)
            {
                queryable = queryable.Where(x => assetRequestParams.CategoryId.Contains(x.CategoryId));
            }
            if (!string.IsNullOrEmpty(assetRequestParams.Query))
                queryable = queryable.Where(x => x.AssetId.Contains(assetRequestParams.Query) || x.AssetName.Contains(assetRequestParams.Query));
            //sort
            var q = queryable.Select(x => new AssetModel()
            {
                AssetId = x.AssetId,
                AssetName = x.AssetName,
                CategoryName = x.Category.CategoryName,
                State = x.State
            });
            q = this.SortData<AssetModel, AssetRequestParams>(q, assetRequestParams);
            //paging
            var result = this.Paging<AssetModel>(q, assetRequestParams.PageSize, assetRequestParams.Page);
            var list = await result.Sources.ToListAsync();
            return (list, result.TotalPage);
        }
        public async Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(AssetAssignmentRequestParams requestParams)
        {
            var queryable = _dbContext.Assets.Include(x => x.Category).AsQueryable();
            queryable = queryable.Where(x => x.LocationId == requestParams.LocationId);
            var location = await _dbContext.Assets.FirstOrDefaultAsync(x => x.LocationId == requestParams.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            queryable = queryable.Where(x => x.State == (short)StateAsset.Avaiable);
            if (!string.IsNullOrEmpty(requestParams.Query))
                queryable = queryable.Where(x => x.AssetId.Contains(requestParams.Query) || x.AssetName.Contains(requestParams.Query) || x.Category.CategoryName.Contains(requestParams.Query));
            var list = await queryable.Select(x => new AssetModel
            {
                AssetId = x.AssetId,
                AssetName = x.AssetName,
                CategoryName = x.Category.CategoryName,
                State = x.State
            }).ToListAsync();

            if (!string.IsNullOrEmpty(requestParams.CurrentAssetId))
            {
                var currentasset = await _dbContext.Assets.Include(x => x.Category).FirstOrDefaultAsync(x => x.AssetId == requestParams.CurrentAssetId);
                if (currentasset == null)
                {
                    throw new Exception("Repository | Have not asset");
                }
                var currentassetmodel = new AssetModel
                {
                    AssetId = currentasset.AssetId,
                    AssetName = currentasset.AssetName,
                    CategoryName = currentasset.Category.CategoryName,
                    State = currentasset.State
                };
                list.Add(currentassetmodel);
            }
            var q = list.AsQueryable();
            q = this.SortData<AssetModel, AssetAssignmentRequestParams>(q, requestParams);
            var finalList = q.ToList();
            return finalList;
        }
        public async Task<ICollection<AssetHistoryModel>> GetListAssetHistoryAsync(string assetId)
        {
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assetId);
            if (asset == null)
            {
                throw new Exception("Repository | Have not asset");
            }
            var assetHistoryModel = await _dbContext.Assignments
                .Where(x => x.AssetId == assetId && x.State == (int)StateAssignment.Completed)
                .Include(x => x.ReturnRequest)
                .Select(x => new AssetHistoryModel
                {
                    AssignmentId = x.AssignmentId,
                    Date = x.AssignedDate.Value,
                    AssignedTo = x.AssignTo,
                    AssignedBy = x.AssignBy,
                    ReturnedDate = x.ReturnRequest.ReturnDate.Value
                }).ToListAsync();
            return assetHistoryModel;
        }
        public async Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest)
        {
            var asset = await _dbContext.Assets.Include(x => x.Category).FirstOrDefaultAsync(x => x.AssetId == id);
            if (asset == null)
            {
                throw new Exception("Repository | Have not asset");
            }
            if (asset.State == (int)StateAsset.Assigned)
            {
                throw new Exception("Repository | State is assigned");
            }
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssetId == assetRequest.AssetId);
            if (assignment != null)
            {
                throw new Exception("Repository | Have assignment");
            }
            asset.AssetName = assetRequest.AssetName;
            asset.Specification = assetRequest.Specification;
            asset.InstalledDate = assetRequest.InstalledDate.Value;
            asset.State = assetRequest.State;
            var assetmodel = new AssetModel
            {
                AssetId = asset.AssetId,
                AssetName = asset.AssetName,
                CategoryName = asset.Category.CategoryName,
                State = asset.State
            };
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return assetmodel;
            }
            throw new Exception("Repository | Update asset fail");
        }
    }
}
