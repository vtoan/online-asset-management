using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AssetRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AssetRequestModel> CreateAssetAsync(AssetRequestModel assetRequest)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == assetRequest.CategoryId);
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
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Assets.Add(asset);
                category.NumIncrease = category.NumIncrease + 1;
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {

            }
            return assetRequest;
        }
        public async Task<bool> DeleteAssetAsync(string id)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssetId == id);
            if (assignment != null)
            {
                return false;
            }
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == id);
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == asset.CategoryId);
            _dbContext.Assets.Remove(asset);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<AssetModel> GetAssetByIdAsync(string id)
        {
            var asset = await _dbContext.Assets.Include(x=>x.Category).FirstOrDefaultAsync(x => x.AssetId == id);
            var assetmodel = new AssetModel
            {
                AssetId = asset.AssetId,
                AssetName = asset.AssetName,
                CategoryName = asset.Category.CategoryName,
                Specification = asset.Specification,
                InstalledDate = asset.InstalledDate.Value,
                State = asset.State
            };
            return assetmodel;
        }

        public async Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(StateAsset? state, string categoryid, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState, string locationid, int page = 1, int pageSize = 10)
        {
            var queryable = _dbContext.Assets.Include(x => x.Category).AsQueryable();
            queryable = queryable.Where(x => x.LocationId == locationid);

            if (state.HasValue)
            {
                queryable = queryable.Where(x => x.State == (short)state.Value);
            }
            if (!string.IsNullOrEmpty(categoryid))
            {
                queryable = queryable.Where(x => x.CategoryId == categoryid);
            }
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.AssetId.Contains(query) || x.AssetName.Contains(query));
            }

            if (sortCode.HasValue)
            {
                if (sortCode.Value == SortBy.ASC)
                {
                    queryable = queryable.OrderBy(x => x.AssetId);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.AssetId);
                }
            }
            else if (sortName.HasValue)
            {
                if (sortName.Value == SortBy.ASC)
                {
                    queryable = queryable.OrderBy(x => x.AssetName);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.AssetName);
                }
            }
            else if (sortCate.HasValue)
            {
                if (sortCate.Value == SortBy.ASC)
                {
                    queryable = queryable.OrderBy(x => x.Category.CategoryName);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.Category.CategoryName);
                }
            }
            else if (sortState.HasValue)
            {
                if (sortState.Value == SortBy.ASC)
                {
                    queryable = queryable.OrderBy(x => x.State);
                }
                else
                {
                    queryable = queryable.OrderByDescending(x => x.State);
                }
            }
            else
            {
            }
            var totalRecord = queryable.Count();
            var list = await queryable.Select(x => new AssetModel
            {
                AssetId = x.AssetId,
                AssetName = x.AssetName,
                CategoryName = x.Category.CategoryName,
                Specification = x.Specification,
                InstalledDate = x.InstalledDate.Value,
                State = x.State
            }).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var totalpage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return (list, totalpage);
        }

        public async Task<AssetRequestModel> UpdateAssetAsync(AssetRequestModel assetRequest)
        {
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assetRequest.AssetId);
            if (asset == null)
            {
                return null;
            }
            asset.AssetName = assetRequest.AssetName;
            asset.Specification = assetRequest.Specification;
            asset.InstalledDate = assetRequest.InstalledDate.Value;
            asset.State = assetRequest.State;
            await _dbContext.SaveChangesAsync();
            return assetRequest;
        }
    }
}
