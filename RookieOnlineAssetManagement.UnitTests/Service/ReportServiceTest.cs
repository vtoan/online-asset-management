using Moq;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Service
{
   public class ReportServiceTest  : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

    public ReportServiceTest(SqliteInMemoryFixture fixture)
    {
        _fixture = fixture;
        _fixture.CreateDatabase();
    }
        [Fact]
        public async Task GetListReport_Success()
        {
            var mockRequestRepo = new Mock<IReportRepository>();
            ReportModel Model = new ReportModel();
            List<ReportModel> collection = new List<ReportModel>();
            int totalP = 0;
            (ICollection<ReportModel> Datas, int totalpage) List = new(collection, totalP);
            mockRequestRepo.Setup(x => x.GetListReportAsync(It.IsAny<ReportRequestParams>())).ReturnsAsync(List);
            var request = new ReportService(mockRequestRepo.Object);
            var requestParams = new ReportRequestParams();
            var result = await request.GetListReportAsync(requestParams);
            Assert.NotNull(result.Datas);
        }

        [Fact]
        public async Task ExportToExcel_Success()
        {
            var mockRequestRepo = new Mock<IReportRepository>();
            ReportModel Model = new ReportModel();
            List<ReportModel> collection = new List<ReportModel>();
            mockRequestRepo.Setup(x => x.ExportReportAsync(It.IsAny<ReportRequestParams>())).ReturnsAsync(collection);
            var request = new ReportService(mockRequestRepo.Object);
            var requestParams = new ReportRequestParams();
            var result = await request.ExportReportAsync(requestParams);
            Assert.NotNull(result);
        }
    }
}
