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
        Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(string currenassetid, string locationid, string query, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort);
        Task<AssetDetailModel> GetAssetByIdAsync(string id);
        Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest);
        Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest);
        Task<bool> DeleteAssetAsync(string id);
    }
}
