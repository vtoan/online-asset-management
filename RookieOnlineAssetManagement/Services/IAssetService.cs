using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IAssetService
    {
        Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(StateAsset? state, string categoryid, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState, string locationid,int page, int pageSize);
        Task<AssetModel> GetAssetByIdAsync(string id);
        Task<AssetRequestModel> CreateAssetAsync(AssetRequestModel assetRequest);
        Task<AssetRequestModel> UpdateAssetAsync(AssetRequestModel assetRequest);
        Task<bool> DeleteAssetAsync(string id);
    }
}