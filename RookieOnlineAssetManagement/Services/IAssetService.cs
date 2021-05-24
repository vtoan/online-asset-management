using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IAssetService
    {
        Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(AssetRequestParams assetRequestParams);
        Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(string currenassetid, string locationid, string query, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort);
        Task<ICollection<AssetHistoryModel>> GetListAssetHistoryAsync(string assetId);
        Task<AssetDetailModel> GetAssetByIdAsync(string id);
        Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest);
        Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest);
        Task<bool> DeleteAssetAsync(string id);
    }
}