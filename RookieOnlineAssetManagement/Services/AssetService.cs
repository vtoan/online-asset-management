using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }
        public async Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(AssetRequestParams assetRequestParams)
        {
            return await _assetRepository.GetListAssetAsync(assetRequestParams);
        }
        public async Task<ICollection<AssetModel>> GetListAssetForAssignmentAsync(AssetAssignmentRequestParams requestParams)
        {
            return await _assetRepository.GetListAssetForAssignmentAsync(requestParams);
        }
        public async Task<ICollection<AssetHistoryModel>> GetListAssetHistoryAsync(string assetId)
        {
            return await _assetRepository.GetListAssetHistoryAsync(assetId);
        }
        public async Task<AssetDetailModel> GetAssetByIdAsync(string id)
        {
            return await _assetRepository.GetAssetByIdAsync(id);
        }
        public async Task<AssetModel> CreateAssetAsync(AssetRequestModel assetRequest)
        {
            return await _assetRepository.CreateAssetAsync(assetRequest);
        }
        public async Task<AssetModel> UpdateAssetAsync(string id, AssetRequestModel assetRequest)
        {
            return await _assetRepository.UpdateAssetAsync(id,assetRequest);
        }
        public async Task<bool> DeleteAssetAsync(string id)
        {
            return await _assetRepository.DeleteAssetAsync(id);
        }
    }
}
