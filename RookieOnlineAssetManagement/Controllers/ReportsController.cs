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
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> ExportAsync(string locationId)
        {
            return Ok(await _reportService.ExportReportAsync(locationId));
        }
        [HttpGet]
        public async Task<ActionResult<ReportModel>> GetListAsync(string locationId)
        {
            return Ok(await _reportService.GetListReportAsync(locationId));
        }
    }
}
