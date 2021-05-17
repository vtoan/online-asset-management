using Microsoft.AspNetCore.Mvc;
using Moq;
using RookieOnlineAssetManagement.Controllers;
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

namespace RookieOnlineAssetManagement.UnitTests.Controller
{
  public  class CategoryControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;



        public CategoryControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task GetCate_Success()
        {
            var mockCateService = new Mock<ICategoryService>();
            var cateModel = new CategoryModel
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Demo",
                ShortName = "DE"
            };
            mockCateService.Setup(m => m.GetListCategoryAsync());
            var cateSer = new CategoryController(mockCateService.Object);
            var result = await cateSer.GetListAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateCate_Success()
        {
            var mockCateService = new Mock<ICategoryService>();
            var cateModel = new CategoryModel
            {
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "Category Demo",
                ShortName = "CD"
            };
            mockCateService.Setup(m => m.CreateCategoryrAsync(It.IsAny<CategoryModel>())).ReturnsAsync(cateModel);
            var cateSer = new CategoryController(mockCateService.Object);
            var result = await cateSer.CreateAsync(cateModel);
            Assert.NotNull(result);
        }
    }
}
 