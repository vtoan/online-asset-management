using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class ReturnRequestsController : ControllerBase
    {
        private readonly IReturnRequestService _returnRequestService;
        public ReturnRequestsController(IReturnRequestService returnRequestService)
        {
            _returnRequestService = returnRequestService;
        }
        [HttpPost]
        public async Task<ActionResult<ReturnRequestModel>> CreateAsync(string assignmentId, string requestedUserId)
        {
            return Ok(await _returnRequestService.CreateReturnRequestAsync(assignmentId, requestedUserId));
        }
        [HttpPut("admin-accept")]
        public async Task<ActionResult<bool>> ChangeStateAcceptAsync(string assignmentId, string acceptedUserId)
        {
            bool accept = true;
            return Ok(await _returnRequestService.ChangeStateAsync(accept, assignmentId, acceptedUserId));
        }
        [HttpPut("admin-cancel")]
        public async Task<ActionResult<bool>> ChangeStateCancelAsync(string assignmentId, string acceptedUserId)
        {
            bool accept = false;
            return Ok(await _returnRequestService.ChangeStateAsync(accept, assignmentId, acceptedUserId));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnRequestModel>>> GetListAsync([FromQuery] ReturnRequestParams returnRequestParams)
        {
            var result = await _returnRequestService.GetListReturnRequestAsync(returnRequestParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            HttpContext.Response.Headers.Add("total-item", result.TotalItem.ToString());
            return Ok(result.Datas);
        }
    }
}
