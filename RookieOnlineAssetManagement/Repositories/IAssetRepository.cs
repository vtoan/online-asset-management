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
        ICollection<AssetModel> GetListAssetAsync(StateAsset state, string category, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState);

        Task<AssetModel> GetAsstByIdAsync(string id);

        Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest);

        Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest);

        Task<AssetModel> DeleteAssetAsync(string id);



    }
}
