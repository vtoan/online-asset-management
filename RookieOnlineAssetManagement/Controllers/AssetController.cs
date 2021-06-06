using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using RookieOnlineAssetManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ADMIN")]
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
            assetRequestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var result = await _assetService.GetListAssetAsync(assetRequestParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("assignment-asset")]
        public async Task<ActionResult<IEnumerable<AssetModel>>> GetListForAssignmentAsync([FromQuery] AssetAssignmentRequestParams requestParams)
        {
            requestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            return Ok(await _assetService.GetListAssetForAssignmentAsync(requestParams));
        }
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<AssetHistoryModel>>> GetListHistoryAsync(string assetId)
        {
            if (string.IsNullOrEmpty(assetId)) return BadRequest("Asset Id is not valid");
            return Ok(await _assetService.GetListAssetHistoryAsync(assetId));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDetailModel>> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Asset Id is not valid");
            return Ok(await _assetService.GetAssetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult<AssetModel>> CreateAsync(AssetRequestModel assetRequestModel)
        {
            assetRequestModel.LocationId = RequestHelper.GetLocationSession(HttpContext);
            if (!ModelState.IsValid) return BadRequest(assetRequestModel);
            return Ok(await _assetService.CreateAssetAsync(assetRequestModel));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AssetModel>> UpdateAsync(string id, AssetRequestModel assetRequestModel)
        {
            assetRequestModel.LocationId = RequestHelper.GetLocationSession(HttpContext);
            if (string.IsNullOrEmpty(id)) return BadRequest("Asset Id is not valid");
            if (!ModelState.IsValid) return BadRequest(assetRequestModel);
            return Ok(await _assetService.UpdateAssetAsync(id, assetRequestModel));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Asset Id is not valid");
            return Ok(await _assetService.DeleteAssetAsync(id));
        }
    }
}
