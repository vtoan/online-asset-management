using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Repositories
{
    public  class CategoryRepoTest :  IClassFixture<SqliteInMemoryFixture>
    {

        private readonly SqliteInMemoryFixture _fixture;

        public CategoryRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task CreateCategory_Failed()
        {
            var dbContext = _fixture.Context;
            var categoryId = Guid.NewGuid().ToString();
            var locationId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            var assetTest = new CategoryModel()
            {
                CategoryId = categoryId,
                CategoryName = "Test",
                ShortName ="T",
            };
            await dbContext.SaveChangesAsync();
            // create repo
            var assetRepo = new CategoryRepository(dbContext);
            var assetNew = await assetRepo.CreateCategoryrAsync(assetTest);
            // test
            Assert.NotNull(assetNew);
        }

        [Fact]
        public async Task CreateCategory_Failed_success()
        {
            var dbContext = _fixture.Context;
            var categoryId = Guid.NewGuid().ToString();
            var locationId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            var assetTest = new CategoryModel()
            {
                CategoryId = categoryId,
                CategoryName = "Test",
                ShortName = "T",
            };
            await dbContext.SaveChangesAsync();
            // create repo
            var assetRepo = new CategoryRepository(dbContext);
            var assetNew = await assetRepo.CreateCategoryrAsync(assetTest);
            // test
            Assert.NotNull(assetNew);
        }
        [Fact]
        public async Task CreateCategory_Success()
        {
            var dbContext = _fixture.Context;
            var categoryId = Guid.NewGuid().ToString();
            var locationId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            var assetTest = new CategoryModel()
            {
                CategoryId = categoryId,
                CategoryName = "category Test",
                ShortName = "ct",
            };
            await dbContext.SaveChangesAsync();
            // create repo
            var assetRepo = new CategoryRepository(dbContext);
            var assetNew = await assetRepo.CreateCategoryrAsync(assetTest);
            // test
            Assert.NotNull(assetNew);
        }

        [Fact]
        public async Task GetCategory_Success()
        {
            var dbContext = _fixture.Context;
            dbContext.Categories.Add(new Category {CategoryId="Ca1",ShortName="TL",CategoryName = "Test Category List" });
            await dbContext.SaveChangesAsync();

            var category = new CategoryRepository(dbContext);
            var result = category.GetListCategoryAsync();
            Assert.NotNull(result);
        }
    }
}
