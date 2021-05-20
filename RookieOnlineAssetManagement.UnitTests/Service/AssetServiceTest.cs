using Moq;
using RookieOnlineAssetManagement.Entities;
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
    public class AssetServiceTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public AssetServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task CreateAsset_Success()
        {
            var mockAssetRepo = new Mock<IAssetRepository>();
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
            mockAssetRepo.Setup(m => m.CreateAssetAsync(It.IsAny<AssetRequestModel>())).ReturnsAsync(assetmodel);
            var assetSer = new AssetService(mockAssetRepo.Object);
            var result = await assetSer.CreateAssetAsync(asset);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UpdateAsset_Success()
        {
            var mockAssetRepo = new Mock<IAssetRepository>();
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
            mockAssetRepo.Setup(x => x.UpdateAssetAsync(It.IsAny<AssetRequestModel>())).ReturnsAsync(assetmodel);
            var assetSer = new AssetService(mockAssetRepo.Object);
            var result = await assetSer.UpdateAssetAsync(asset);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteAsset_Success()
        {
            var mockAssetRepo = new Mock<IAssetRepository>();
            string assetid = Guid.NewGuid().ToString();
            mockAssetRepo.Setup(x => x.DeleteAssetAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assetSer = new AssetService(mockAssetRepo.Object);
            var result = await assetSer.DeleteAssetAsync(assetid);
            Assert.True(result);
        }
        [Fact]
        public async Task GetAssetById_Success()
        {
            var mockAssetRepo = new Mock<IAssetRepository>();
            string assetid = Guid.NewGuid().ToString();
            AssetDetailModel assetdetailModel = new AssetDetailModel();
            mockAssetRepo.Setup(x => x.GetAssetByIdAsync(It.IsAny<string>())).ReturnsAsync(assetdetailModel);
            var assetSer = new AssetService(mockAssetRepo.Object);
            var result = await assetSer.GetAssetByIdAsync(assetid);
            Assert.NotNull(result);
        }
    }
}
