﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using RookieOnlineAssetManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult<ReturnRequestModel>> CreateAsync(string assignmentId)
        {
            if (string.IsNullOrEmpty(assignmentId)) return BadRequest("Assignment Id is not valid ");
            var requestedUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _returnRequestService.CreateReturnRequestAsync(assignmentId, requestedUserId));
        }
        [HttpPut("accept/{assignmentId}")]
        [Authorize("ADMIN")]
        public async Task<ActionResult<bool>> ChangeStateAcceptAsync(string assignmentId)
        {
            if (string.IsNullOrEmpty(assignmentId)) return BadRequest("Assignment Id is not valid "); ;
            bool accept = true;
            var acceptedUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _returnRequestService.ChangeStateAsync(accept, assignmentId, acceptedUserId));
        }
        [HttpPut("cancel/{assignmentId}")]
        [Authorize("ADMIN")]
        public async Task<ActionResult<bool>> ChangeStateCancelAsync(string assignmentId)
        {
            if (string.IsNullOrEmpty(assignmentId)) return BadRequest("Assignment Id is not valid ");
            bool cancel = false;
            var acceptedUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _returnRequestService.ChangeStateAsync(cancel, assignmentId, acceptedUserId));
        }
        [HttpGet]
        [Authorize("ADMIN")]
        public async Task<ActionResult<IEnumerable<ReturnRequestModel>>> GetListAsync([FromQuery] ReturnRequestParams returnRequestParams)
        {
            returnRequestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var result = await _returnRequestService.GetListReturnRequestAsync(returnRequestParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            HttpContext.Response.Headers.Add("total-item", result.TotalItem.ToString());
            return Ok(result.Datas);
        }
    }
}
