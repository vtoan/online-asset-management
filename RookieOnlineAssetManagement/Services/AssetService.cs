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
        public async Task<(ICollection<AssetModel> Datas, int TotalPage)> GetListAssetAsync(StateAsset[] state, string[] categoryid, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState, string locationid, int page, int pageSize)
        {
            return await _assetRepository.GetListAssetAsync(state, categoryid, query, sortCode, sortName, sortCate, sortState, locationid, page, pageSize);
        }
        public async Task<AssetModel> GetAssetByIdAsync(string id)
        {
            return await _assetRepository.GetAssetByIdAsync(id);
        }
        public async Task<AssetRequestModel> CreateAssetAsync(AssetRequestModel assetRequest)
        {
            return await _assetRepository.CreateAssetAsync(assetRequest);
        }
        public async Task<AssetRequestModel> UpdateAssetAsync(AssetRequestModel assetRequest)
        {
            return await _assetRepository.UpdateAssetAsync(assetRequest);
        }
        public async Task<bool> DeleteAssetAsync(string id)
        {
            return await _assetRepository.DeleteAssetAsync(id);
        }
    }
}
