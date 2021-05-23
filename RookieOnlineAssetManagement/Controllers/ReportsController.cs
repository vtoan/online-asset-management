using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ActionResult<ICollection<ReportModel>>> ExportAsync(string locationId)
        {
            var report = await _reportService.ExportReportAsync(locationId);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report Category");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Style.Font.SetBold();
                worksheet.Cell(currentRow, 2).Style.Font.SetBold();
                worksheet.Cell(currentRow, 3).Style.Font.SetBold();
                worksheet.Cell(currentRow, 4).Style.Font.SetBold();
                worksheet.Cell(currentRow, 5).Style.Font.SetBold();
                worksheet.Cell(currentRow, 6).Style.Font.SetBold();
                worksheet.Cell(currentRow, 7).Style.Font.SetBold();
                worksheet.Cell(currentRow, 1).Value = "Category";
                worksheet.Cell(currentRow, 2).Value = "Total";
                worksheet.Cell(currentRow, 3).Value = "AssignedTotal";
                worksheet.Cell(currentRow, 4).Value = "AvailableTotal";
                worksheet.Cell(currentRow, 5).Value = "NotAvailableTotal";
                worksheet.Cell(currentRow, 6).Value = "WatingRecyclingTotal";
                worksheet.Cell(currentRow, 7).Value = "RecycledTotal";
                foreach (var item in report)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.CategoryName;
                    worksheet.Cell(currentRow, 2).Value = item.Total;
                    worksheet.Cell(currentRow, 3).Value = item.AssignedTotal;
                    worksheet.Cell(currentRow, 4).Value = item.AvailableTotal;
                    worksheet.Cell(currentRow, 5).Value = item.NotAvailableTotal;
                    worksheet.Cell(currentRow, 6).Value = item.WatingRecyclingTotal;
                    worksheet.Cell(currentRow, 7).Value = item.RecycledTotal;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reports.xlsx");
                }
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportModel>>> GetListAsync(string locationId)
        {
            return Ok(await _reportService.GetListReportAsync(locationId));
        }
    }
}
