using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetModel>>> GetListAsync([FromQuery] AssetRequestParams assetRequestParams)
        {
            var result = await _assetService.GetListAssetAsync(assetRequestParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("assignment-asset")]
        public async Task<ActionResult<IEnumerable<AssetModel>>> GetListForAssignmentAsync(string currentAssetId, string locationId, string query, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort)
        {
            return Ok(await _assetService.GetListAssetForAssignmentAsync(currentAssetId, locationId, query, AssetIdSort, AssetNameSort, CategoryNameSort));
        }
        [HttpGet("history-asset")]
        public async Task<ActionResult<IEnumerable<AssetHistoryModel>>> GetListHistoryAsync(string assetId)
        {
            return Ok(await _assetService.GetListAssetHistoryAsync(assetId));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDetailModel>> GetAsync(string id)
        {
            return Ok(await _assetService.GetAssetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<AssetModel>> CreateAsync(AssetRequestModel assetRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest(assetRequestModel);
            return Ok(await _assetService.CreateAssetAsync(assetRequestModel));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AssetModel>> UpdateAsync(string id, AssetRequestModel assetRequestModel)
        {
            if (!ModelState.IsValid) return BadRequest(assetRequestModel);
            return Ok(await _assetService.UpdateAssetAsync(id, assetRequestModel));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return Ok(await _assetService.DeleteAssetAsync(id));
        }
    }
}
