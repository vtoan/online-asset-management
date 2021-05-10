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
        public Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> DeleteAssetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> GetAsstByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AssetModel> GetListAssetAsync(StateAsset state, string category, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState)
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest)
        {
            throw new NotImplementedException();
        }
    }
}
