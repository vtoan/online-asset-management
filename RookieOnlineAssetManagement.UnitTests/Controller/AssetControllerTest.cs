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
            var asset = new AssetRequestModel
            {
                LocationId = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
                AssetName = "Asset Test",
                InstalledDate = new DateTime(),
                State = 1,
                Specification = "test",
            };
            mockAssetSer.Setup(m => m.CreateAssetAsync(It.IsAny<AssetRequestModel>())).ReturnsAsync(asset);
            var assetcontr = new AssetController(mockAssetSer.Object);
            var result = await assetcontr.CreateAsync(asset);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Update_Success()
        {
            var mockAssetSer = new Mock<IAssetService>();
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
            mockAssetSer.Setup(x => x.UpdateAssetAsync(It.IsAny<AssetRequestModel>())).ReturnsAsync(asset);
            var assetContr = new AssetController(mockAssetSer.Object);
            var result = await assetContr.UpdateAsync(asset);
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
            AssetModel assetModel = new AssetModel();
            mockAssetSer.Setup(x => x.GetAssetByIdAsync(It.IsAny<string>())).ReturnsAsync(assetModel);
            var assetContr = new AssetController(mockAssetSer.Object);
            var result = await assetContr.GetAsync(assetid);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}
