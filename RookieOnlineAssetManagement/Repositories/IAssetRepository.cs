using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IAssetRepository
    {
        ICollection<AssetModel> GetListAsset(StateAsset state, string category, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState);

        Task<AssetModel> GetAsstById(string id);

        Task<AssetModel> CreateAsset(AssetRequestModel assetRequest);

        Task<AssetModel> UpdateAsset(string id, AssetRequestModel assetRequest);

        Task<AssetModel> DeleteAsset(string id);



    }
}
