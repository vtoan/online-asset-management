using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Repositories
{
   public class ReturnRequestRepoTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public ReturnRequestRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task GetListReturnRequest_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var AssetId = Guid.NewGuid().ToString();
            var Assignid = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var user = new User
            {

                Id = userId,
            };
            var admin = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName = "admin",
                LastName = "hcm",
                Gender = true,
                JoinedDate = DateTime.Now,
                IsChange = true,
                StaffCode = "SD00002",
                LocationId = locationId,
            };
            dbContext.Users.Add(admin);
            await dbContext.SaveChangesAsync();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                CategoryId = categoryId,
                AssetId = AssetId,
                AssetName = "Test",
            };
            dbContext.Assets.Add(asset);
            var assignment = new Assignment
            {
                UserId = user.Id,
                AssignmentId = Assignid,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
            };
            dbContext.Assignments.Add(assignment);
            await dbContext.SaveChangesAsync();
            var returnRequestModel = new ReturnRequestModel
            {
                AssignmentId = assignment.AssignmentId,
                AssetId = AssetId,
                AssetName = "User",
                ReturnedDate = DateTime.Now,  
            };

            var request = new ReturnRequestRepository(dbContext);
            var CreateRequest = await request.CreateReturnRequestAsync(assignment.AssignmentId, userId);
            var Params = new ReturnRequestParams
            {
                LocationId = locationId,
                SortAssetId = Enums.SortBy.ASC,
            };
            var RequestParam = await request.GetListReturnRequestAsync(Params);
            Assert.NotNull(Params);
            Assert.IsType<List<ReturnRequestModel>>(RequestParam.Datas);
            Assert.True(RequestParam.TotalPage >= 0);
        }
    }
}
