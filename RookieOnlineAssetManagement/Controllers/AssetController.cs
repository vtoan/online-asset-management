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
        public async Task<ActionResult<IEnumerable<AssetModel>>> GetListAsync([FromQuery] StateAsset[] state, [FromQuery] string[] categoryid, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortCate, SortBy? sortState, string locationid, int page, int pageSize)
        {
            var result = await _assetService.GetListAssetAsync(state, categoryid, query, sortCode, sortName, sortCate, sortState, locationid, page, pageSize);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
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
