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
using System.Security.Claims;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ADMIN")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        public AssignmentsController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }
        [HttpPost]
        public async Task<ActionResult<AssignmentModel>> CreateAsync(AssignmentRequestModel assignmentRequestModel)
        {
            assignmentRequestModel.LocationId = RequestHelper.GetLocationSession(HttpContext);
            assignmentRequestModel.AdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _assignmentService.CreateAssignmentAsync(assignmentRequestModel));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentModel>> UpdateAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            assignmentRequestModel.LocationId = RequestHelper.GetLocationSession(HttpContext);
            assignmentRequestModel.AdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _assignmentService.UpdateAssignmentAsync(id, assignmentRequestModel));
        }
        [HttpPut("accept/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangeStateAcceptAsync(string id)
        {
            return Ok(await _assignmentService.ChangeStateAssignmentAsync(id, StateAssignment.Accepted));
        }
        [HttpPut("decline/{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangeStateDeclineAsync(string id)
        {
            return Ok(await _assignmentService.ChangeStateAssignmentAsync(id, StateAssignment.Decline));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
            return Ok(await _assignmentService.DeleteAssignmentAsync(id));
        }
        [HttpGet("my-assignments")]
        [Authorize]
        public async Task<ActionResult<MyAssigmentModel>> GetMyListAsync([FromQuery] MyAssignmentRequestParams myAssignmentRequestParams)
        {
            myAssignmentRequestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            myAssignmentRequestParams.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _assignmentService.GetListMyAssignmentAsync(myAssignmentRequestParams));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentModel>>> GetListAsync([FromQuery] AssignmentRequestParams assignmentRequestParams)
        {
            assignmentRequestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var result = await _assignmentService.GetListAssignmentAsync(assignmentRequestParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            HttpContext.Response.Headers.Add("total-item", result.TotalItem.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDetailModel>> GetAssignmentById(string id)
        {
            return Ok(await _assignmentService.GetAssignmentById(id));
        }
    }
}
