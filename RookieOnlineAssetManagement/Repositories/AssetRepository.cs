using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
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
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == assetRequest.CategoryId);
            if (category == null)
            {
                throw new Exception("Repository | Have not this category");
            }
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assetRequest.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
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
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assetRequestParams.LocationId);
            if(location==null)
            {
                throw new Exception("Repository | Have not this loaction");
            }
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
            if (assetRequestParams.SortCode.HasValue)
            {
                if (assetRequestParams.SortCode.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetId);
            }
            else if (assetRequestParams.SortName.HasValue)
            {
                if (assetRequestParams.SortName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetName);
            }
            else if (assetRequestParams.SortCate.HasValue)
            {
                if (assetRequestParams.SortCate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Category.CategoryName);
                else
                    queryable = queryable.OrderByDescending(x => x.Category.CategoryName);
            }
            else if (assetRequestParams.SortState.HasValue)
            {
                if (assetRequestParams.SortState.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.State);
                else
                    queryable = queryable.OrderByDescending(x => x.State);
            }

            var result = Paging<Asset>(queryable, assetRequestParams.PageSize, assetRequestParams.Page);
            var list = await result.Sources.Select(x => new AssetModel
            {
                AssetId = x.AssetId,
                AssetName = x.AssetName,
                CategoryName = x.Category.CategoryName,
                State = x.State
            }).ToListAsync();
            return (list, result.TotalPage);
        }
        public async Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(string currentassetid, string locationid, string query, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort)
        {
            var queryable = _dbContext.Assets.Include(x => x.Category).AsQueryable();
            queryable = queryable.Where(x => x.LocationId == locationid);
            var location = await _dbContext.Assets.FirstOrDefaultAsync(x => x.LocationId == locationid);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            queryable = queryable.Where(x => x.State == (short)StateAsset.Avaiable);
            if (!string.IsNullOrEmpty(query))
                queryable = queryable.Where(x => x.AssetId.Contains(query) || x.AssetName.Contains(query) || x.Category.CategoryName.Contains(query));
            var list = await queryable.Select(x => new AssetModel
            {
                AssetId = x.AssetId,
                AssetName = x.AssetName,
                CategoryName = x.Category.CategoryName,
                State = x.State
            }).ToListAsync();

            if (!string.IsNullOrEmpty(currentassetid))
            {
                var currentasset = await _dbContext.Assets.Include(x => x.Category).FirstOrDefaultAsync(x => x.AssetId == currentassetid);
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

            if (AssetIdSort.HasValue)
            {
                if (AssetIdSort.Value == SortBy.ASC)
                    list = list.OrderBy(x => x.AssetId).ToList();
                else
                    list = list.OrderByDescending(x => x.AssetId).ToList();
            }
            else if (AssetNameSort.HasValue)
            {
                if (AssetNameSort.Value == SortBy.ASC)
                    list = list.OrderBy(x => x.AssetName).ToList();
                else
                    list = list.OrderByDescending(x => x.AssetName).ToList();
            }
            else if (CategoryNameSort.HasValue)
            {
                if (CategoryNameSort.Value == SortBy.ASC)
                    list = list.OrderBy(x => x.CategoryName).ToList();
                else
                    list = list.OrderByDescending(x => x.CategoryName).ToList();
            }
            else
            {
                list = list.OrderBy(x => x.AssetId).ToList();
            }
            return list;
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
