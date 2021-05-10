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
        public ICollection<AssetModel> GetListAsset()
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> CreateAsset(AssetRequestModel assetRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> DeleteAsset(string id)
        {
            throw new NotImplementedException();
        }

        public Task<AssetModel> GetAsstById(string id)
        {
            throw new NotImplementedException();
        }

       
        public Task<AssetModel> UpdateAsset(string id, AssetRequestModel assetRequest)
        {
            throw new NotImplementedException();
        }

        public ICollection<AssetModel> GetListAsset(StateAsset state, string category, string query, SortBy sortCode, SortBy sortName, int sortCate, SortBy sortState)
        {
            var data = new List<AssetModel>();
            var result = new List<AssetModel>();

            if()

            throw new NotImplementedException();
        }
    }
}
