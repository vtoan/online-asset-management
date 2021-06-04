using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
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
   public class ReportRepoTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public ReportRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task GetListReport_Susscess()
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
                State = (int)StateAssignment.Accepted
            };
            dbContext.Assignments.Add(assignment);
            await dbContext.SaveChangesAsync();
            var returnRequestModel = new ReturnRequest
            {
                AssignmentId = assignment.AssignmentId,
                AcceptedBy = "Hung",
                AcceptedUserId = userId,
                ReturnDate = new DateTime(),
                RequestUserId = user.Id,
            };
            dbContext.ReturnRequests.Add(returnRequestModel);
            await dbContext.SaveChangesAsync();

            var Report = new ReportRepository(dbContext);
            var Params = new ReportRequestParams
            {
                LocationId = locationId,
                SortTotal = Enums.SortBy.ASC,
                
            };

            var RequestParam = await Report.GetListReportAsync(Params);
            Assert.NotNull(Params);
            Assert.IsType<List<ReportModel>>(RequestParam.Datas);
            Assert.True(RequestParam.TotalPage >= 0);
        }
        [Fact]
        public async Task ExportReport_Susscess()
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
                State = (int)StateAssignment.Accepted
            };
            dbContext.Assignments.Add(assignment);
            await dbContext.SaveChangesAsync();
            var returnRequestModel = new ReturnRequest
            {
                AssignmentId = assignment.AssignmentId,
                AcceptedBy = "Hung",
                AcceptedUserId = userId,
                ReturnDate = new DateTime(),
                RequestUserId = user.Id,
            };
            dbContext.ReturnRequests.Add(returnRequestModel);
            await dbContext.SaveChangesAsync();

            var Report = new ReportRepository(dbContext);
            var Params = new ReportRequestParams
            {
                LocationId = locationId,
                SortTotal = Enums.SortBy.ASC,

            };
            var RequestParam = await Report.ExportReportAsync(Params);
            Assert.NotNull(Params);
            Assert.IsType<List<ReportModel>>(RequestParam);
        }
    }
}
