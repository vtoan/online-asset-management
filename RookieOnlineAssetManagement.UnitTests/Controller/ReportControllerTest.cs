using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RookieOnlineAssetManagement.Controllers;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Controller
{
   public class ReportControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public ReportControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task ExportReport_Success()
        {
            var HttpContext = new DefaultHttpContext();
            Mock<ISession> sessionMock = new Mock<ISession>();
            var mockService = new Mock<IReportService>();
            List<ReportModel> collection = new List<ReportModel>();
            mockService.Setup(x => x.ExportReportAsync(It.IsAny<ReportRequestParams>())).ReturnsAsync(collection);
            var Request = new ReportRequestParams();
            var controller = new ReportsController(mockService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = HttpContext,
                }
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await controller.ExportAsync(Request);
            Assert.Null(result);
        }
        [Fact]
        public async Task GetListReport_Success()
        {
            var HttpContext = new DefaultHttpContext();
            Mock<ISession> sessionMock = new Mock<ISession>();
            HttpContext.Request.Headers["total-pages"] = "0";
            var mokService = new Mock<IReportService>();
            List<ReportModel> collection = new List<ReportModel>();
            int totalP = 0;
            (ICollection<ReportModel> Datas, int totalpage) List = new(collection, totalP);
            mokService.Setup(x => x.GetListReportAsync(It.IsAny<ReportRequestParams>())).ReturnsAsync(List);
            ReportModel model = new ReportModel();
            var Request = new ReportRequestParams();
            var controller = new ReportsController(mokService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = HttpContext,
                }
            };
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await controller.GetListAsync(Request);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}
