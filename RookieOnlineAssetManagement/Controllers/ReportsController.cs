using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using RookieOnlineAssetManagement.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ADMIN")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("export")]
        public async Task<ActionResult<ICollection<ReportModel>>> ExportAsync([FromQuery]ReportRequestParams reportRequestParams)
        {
            reportRequestParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var report = await _reportService.ExportReportAsync(reportRequestParams);
            return ExportFileHelper.ExportExcel(report, "Report Category", "Reports");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportModel>>> GetListAsync([FromQuery]ReportRequestParams reportParams)
        {
            reportParams.LocationId = RequestHelper.GetLocationSession(HttpContext);
            var result = await _reportService.GetListReportAsync(reportParams);
            HttpContext.Response.Headers.Add("total-pages", result.TotalPage.ToString());
            return Ok(result.Datas);
        }
    }
}
