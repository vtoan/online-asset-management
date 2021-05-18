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
    public class CategoryServiceTest :IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public CategoryServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task CreateCategory_Success()
        {
            var mockCateRepo = new Mock<ICategoryRepository>();
            var cateModel = new CategoryModel
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Demo",
                ShortName="DE"
            };
            mockCateRepo.Setup(m => m.CreateCategoryrAsync(It.IsAny<CategoryModel>())).ReturnsAsync(cateModel);
            var cateSer = new CategoryService(mockCateRepo.Object);
            var result = await cateSer.CreateCategoryrAsync(cateModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetGetCategory_Success()
        {
            var mockCateRepo = new Mock<ICategoryRepository>();
            var cateModel = new CategoryModel
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Demo",
                ShortName = "DE"
            };
            mockCateRepo.Setup(m => m.CreateCategoryrAsync(It.IsAny<CategoryModel>())).ReturnsAsync(cateModel);
            var cateSer = new CategoryService(mockCateRepo.Object);
            var result = await cateSer.CreateCategoryrAsync(cateModel);
            mockCateRepo.Setup(m => m.GetListCategoryAsync());
            var cateService = new CategoryService(mockCateRepo.Object);
            var results = await cateService.GetListCategoryAsync();
            Assert.NotNull(result);

        }
    }
}
