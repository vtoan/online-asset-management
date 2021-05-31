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
    public class AssetControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public AssetControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task Create_Success()
        {
            var mockAssetSer = new Mock<IAssetService>();
            Mock<ISession> sessionMock = new Mock<ISession>();
            var asset = new AssetRequestModel
            {
                LocationId = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
                AssetName = "Asset Test",
                InstalledDate = new DateTime(),
                State = 1,
                Specification = "test",
            };
            var assetmodel = new AssetModel();
            mockAssetSer.Setup(m => m.CreateAssetAsync(It.IsAny<AssetRequestModel>())).ReturnsAsync(assetmodel);
            var assetcontr = new AssetController(mockAssetSer.Object);
            assetcontr.ControllerContext.HttpContext = new DefaultHttpContext();
            assetcontr.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await assetcontr.CreateAsync(asset);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Update_Success()
        {
            var mockAssetSer = new Mock<IAssetService>();
            Mock<ISession> sessionMock = new Mock<ISession>();
            var asset = new AssetRequestModel
            {
                AssetId = "LT100004",
                LocationId = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
                AssetName = "Asset Test",
                InstalledDate = new DateTime(),
                State = 1,
                Specification = "test",
            };
            var assetmodel = new AssetModel();
            mockAssetSer.Setup(x => x.UpdateAssetAsync(It.IsAny<string>(), It.IsAny<AssetRequestModel>())).ReturnsAsync(assetmodel);
            var assetContr = new AssetController(mockAssetSer.Object);
            assetContr.ControllerContext.HttpContext = new DefaultHttpContext();
            assetContr.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await assetContr.UpdateAsync(asset.AssetId, asset);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_Success()
        {
            var mockAssetSer = new Mock<IAssetService>();
            string assetid = Guid.NewGuid().ToString();
            mockAssetSer.Setup(x => x.DeleteAssetAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assetContr = new AssetController(mockAssetSer.Object);
            var result = await assetContr.DeleteAsync(assetid);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task Get_Success()
        {
            var mockAssetSer = new Mock<IAssetService>();
            string assetid = Guid.NewGuid().ToString();
            AssetDetailModel assetdetailModel = new AssetDetailModel();
            mockAssetSer.Setup(x => x.GetAssetByIdAsync(It.IsAny<string>())).ReturnsAsync(assetdetailModel);
            var assetContr = new AssetController(mockAssetSer.Object);
            var result = await assetContr.GetAsync(assetid);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetListHistory_Success()
        {
            var HttpContext = new DefaultHttpContext();
            string assetid = Guid.NewGuid().ToString();
            Mock<ISession> sessionMock = new Mock<ISession>();
            var mokService = new Mock<IAssetService>();
            List<AssetHistoryModel> collection = new List<AssetHistoryModel>();
            mokService.Setup(x => x.GetListAssetHistoryAsync(It.IsAny<string>())).ReturnsAsync(collection);
            AssetHistoryModel model = new AssetHistoryModel();
            var controller = new AssetController(mokService.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await controller.GetListHistoryAsync(assetid);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}
