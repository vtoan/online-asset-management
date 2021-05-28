using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public class AssignmentRepoTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public AssignmentRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task Create_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                AssetId = "LD100012",
                AssetName = "test",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(asset);
            var result = await dbContext.SaveChangesAsync();
            
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName="duyen",
                LastName="le",
                Gender=true,
                JoinedDate=DateTime.Now,
                IsChange=true,
                StaffCode="SD00001",
                LocationId = locationId,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
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
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
            };
            var assignmentRepo = new AssignmentRepository(dbContext);
            var AssignmentModel = await assignmentRepo.CreateAssignmentAsync(assignmentrequsetmodel);

            Assert.IsType<AssignmentModel>(AssignmentModel);
            Assert.NotNull(AssignmentModel);
            Assert.NotSame(AssignmentModel.AssignmentId, assignmentrequsetmodel.AssignmentId);
        }
        [Fact]
        public async Task Update_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                AssetId = "LD100012",
                AssetName = "test",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(asset);
            var result = await dbContext.SaveChangesAsync();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName = "duyen",
                LastName = "le",
                Gender = true,
                JoinedDate = DateTime.Now,
                IsChange = true,
                StaffCode = "SD00001",
                LocationId = locationId,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
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
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
            };
            var assignmentRepo = new AssignmentRepository(dbContext);
            var AssignmentModel = await assignmentRepo.CreateAssignmentAsync(assignmentrequsetmodel);

            var assetupdate = new Asset
            {
                AssetId = "LD100013",
                AssetName = "testupdate",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(assetupdate);
            await dbContext.SaveChangesAsync();
            var assignmentrequsetmodelupdate = new AssignmentRequestModel
            {
                AssignmentId = AssignmentModel.AssignmentId,
                UserId = user.Id,
                AssetId = assetupdate.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
                State = (int)StateAssignment.WaitingForAcceptance
            };
            var AssignmentModelUpdate = await assignmentRepo.UpdateAssignmentAsync(assignmentrequsetmodelupdate.AssignmentId, assignmentrequsetmodelupdate);

            Assert.NotNull(AssignmentModelUpdate);
            Assert.IsType<AssignmentModel>(AssignmentModelUpdate);
            Assert.NotSame(AssignmentModelUpdate.AssetId, AssignmentModel.AssetId);
        }
        [Fact]
        public async Task Delete_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                AssetId = "LD100012",
                AssetName = "test",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(asset);
            var result = await dbContext.SaveChangesAsync();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName = "duyen",
                LastName = "le",
                Gender = true,
                JoinedDate = DateTime.Now,
                IsChange = true,
                StaffCode = "SD00001",
                LocationId = locationId,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
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
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
            };
            var assignmentRepo = new AssignmentRepository(dbContext);
            var AssignmentModel = await assignmentRepo.CreateAssignmentAsync(assignmentrequsetmodel);

            var deleted = await assignmentRepo.DeleteAssignmentAsync(AssignmentModel.AssignmentId);
            var item = dbContext.Assignments.FirstOrDefault(x => x.AssignmentId == AssignmentModel.AssignmentId);

            Assert.Null(item);
            Assert.True(deleted);
        }
        [Fact]
        public async Task GetAssignmetById_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                AssetId = "LD100012",
                AssetName = "test",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(asset);
            var result = await dbContext.SaveChangesAsync();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName = "duyen",
                LastName = "le",
                Gender = true,
                JoinedDate = DateTime.Now,
                IsChange = true,
                StaffCode = "SD00001",
                LocationId = locationId,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
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
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
            };
            var assignmentRepo = new AssignmentRepository(dbContext);
            var AssignmentModel = await assignmentRepo.CreateAssignmentAsync(assignmentrequsetmodel);

            var assign = await assignmentRepo.GetAssignmentById(AssignmentModel.AssignmentId);

            Assert.True(assign.AssignmentId.Equals(AssignmentModel.AssignmentId));
            Assert.NotNull(assign);
            Assert.IsNotType<AssignmentDetailModel>(asset);
        }
        [Fact]
        public async Task GetAssignment_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            // add mock data
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            dbContext.Categories.Add(new Category() { CategoryId = categoryId, CategoryName = "Laptop", ShortName = "LA" });
            await dbContext.SaveChangesAsync();
            var asset = new Asset
            {
                AssetId = "LD100012",
                AssetName = "test",
                CategoryId = categoryId,
                State = (short)StateAsset.Avaiable,
                Specification = "test",
                InstalledDate = DateTime.Now,
                LocationId = locationId
            };
            dbContext.Assets.Add(asset);
            var result = await dbContext.SaveChangesAsync();

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AssignedToUser",
                FirstName = "duyen",
                LastName = "le",
                Gender = true,
                JoinedDate = DateTime.Now,
                IsChange = true,
                StaffCode = "SD00001",
                LocationId = locationId,
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
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
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                AssetId = asset.AssetId,
                AdminId = admin.Id,
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = locationId,
            };
            var assignmentRepo = new AssignmentRepository(dbContext);
            var AssignmentModel = await assignmentRepo.CreateAssignmentAsync(assignmentrequsetmodel);
            var param = new AssignmentRequestParams
            {
                LocationId = AssignmentModel.LocationId,
                SortAssetId = SortBy.ASC,

            };
            var assign = await assignmentRepo.GetListAssignmentAsync(param);
            Assert.IsType<List<AssignmentModel>>(assign.Datas);
            Assert.True(assign.TotalPage >= 0);
            Assert.True(assign.TotalItem > 0);
        }
    }
}
