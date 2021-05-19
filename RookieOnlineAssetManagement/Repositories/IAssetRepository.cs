using Microsoft.AspNetCore.Mvc;
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
        Task<(ICollection<AssetModel> Datas ,int TotalPage)> GetListAssetAsync(StateAsset[] state, string[] categoryid, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState, string locationid, int page, int pageSize);

        Task<AssetDetailModel> GetAssetByIdAsync(string id);

        Task<AssetRequestModel> CreateAssetAsync(AssetRequestModel assetRequest);

        Task<AssetRequestModel> UpdateAssetAsync(AssetRequestModel assetRequest);

        Task<bool> DeleteAssetAsync(string id);



    }
}
