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
        [HttpPut]
        public async Task<ActionResult<bool>> ChangeStateAsync(bool accept, string assignmentId, string acceptedUserId)
        {
            return Ok(await _returnRequestService.ChangeStateAsync(accept, assignmentId, acceptedUserId));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnRequestModel>>> GetListAsync([FromQuery] ReturnRequestParams returnRequestParams)
        {
            return Ok(await _returnRequestService.GetListReturnRequestAsync(returnRequestParams));
        }
    }
}
