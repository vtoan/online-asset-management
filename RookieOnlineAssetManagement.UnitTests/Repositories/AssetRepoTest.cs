using System;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Repositories
{
    public class AssetRepoTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public AssetRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task CreateAsset_Success()
        {
            // initial mock data
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            //
            var assetTest = new AssetRequestModel()
            {
                CategoryId = categoryId,
                LocationId = locationId,
                AssetName = "Asset Test",
                InstalledDate = new DateTime(),
            };
            // create repo
            var assetRepo = new AssetRepository(dbContext);
            var assetNew = await assetRepo.CreateAssetAsync(assetTest);
            // test
            Assert.NotNull(assetNew);
        }
    }
}