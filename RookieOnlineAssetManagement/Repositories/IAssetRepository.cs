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
        Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(AssetRequestParams assetRequestParams);
        Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(AssetAssignmentRequestParams requestParams);
        Task<ICollection<AssetHistoryModel>> GetListAssetHistoryAsync(string assetId);
        Task<AssetDetailModel> GetAssetByIdAsync(string id);
        Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest);
        Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest);
        Task<bool> DeleteAssetAsync(string id);
    }
}
