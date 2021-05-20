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
            return Ok(await _assignmentService.CreateAssignmentAsync(assignmentRequestModel));
        }
        [HttpPut("{id})")]
        public async Task<ActionResult<AssignmentModel>> UpdateAsync(string id,AssignmentRequestModel assignmentRequestModel)
        {
            return Ok(await _assignmentService.UpdateAssignmentAsync(id, assignmentRequestModel));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAssignmentAsync(string id)
        {
            return Ok( await _assignmentService.DeleteAssignmentAsync(id));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentModel>>> GetListAsync([FromQuery] StateAssignment[] StateAssignments, [FromQuery] string AssignedDateAssignment, string query, SortBy AssetId, SortBy AssetName, SortBy AssignedTo, SortBy AssignedBy, SortBy AssignedDate, SortBy State, int page, int pageSize)
        {
            var result = await _assignmentService.GetListAssignmentAsync(StateAssignments, AssignedDateAssignment, query, AssetId, AssetName, AssignedTo, AssignedBy, AssignedDate, State, page, pageSize);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            HttpContext.Response.Headers.Add("total-item", result.TotalItem.ToString());
            return Ok(result.Datas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AssetDetailModel>> GetAssignmentById(string id)
        {
            return Ok(await _assignmentService.GetAssignmentById(id));
        }
    }
}
