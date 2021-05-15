using Microsoft.AspNetCore.Mvc;
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
            var dbContext = _fixture.Context;
            var category = new Category { CategoryName = "Product Category", CategoryId = "ca1" ,ShortName ="CA"};
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();
            // create dependency
            var cateDao = new CategoryRepository(dbContext);
            var cateService = new CategoryService(cateDao);
            // test
            var cateController = new CategoryController(cateService);
            var result = cateController.GetListAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateCate_Success()
        {
            // initial mock data
            var dbContext = _fixture.Context;
            // create dependency
            var cateRepo = new CategoryRepository(dbContext);
            var cateService = new CategoryService(cateRepo);
            // test
            var category= new CategoryModel { CategoryName = "Create Category Test", CategoryId = "CT",ShortName="CA" };
            var cateController = new CategoryController(cateService);
            var result = await cateController.CreateAsync(category);

            Assert.IsType<ActionResult<CategoryModel>> (result);
            Assert.NotNull(result);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(actionResult);
        }
    }
}
 