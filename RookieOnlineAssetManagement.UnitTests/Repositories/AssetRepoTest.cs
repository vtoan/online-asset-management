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
            var locationid = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            var assetid = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationid, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            //
            var assetTest = new AssetRequestModel()
            {
                CategoryId = categoryId,
                AssetName = "Asset Test",
                InstalledDate = new DateTime(),
                LocationId = locationid
            };
            // create repo
            var assetRepo = new AssetRepository(dbContext);
            var assetNew = await assetRepo.CreateAssetAsync(assetTest);
            // test
            var resultAsset = await assetRepo.GetAssetByIdAsync(assetNew.AssetId);
            Assert.True(assetid == resultAsset.AssetId);
        }
    }
}