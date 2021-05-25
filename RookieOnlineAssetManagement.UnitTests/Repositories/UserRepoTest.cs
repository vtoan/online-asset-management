using Microsoft.AspNetCore.Identity;
using Moq;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using RookieOnlineAssetManagement.UnitTests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Repositories
{
    public class UserRepoTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public UserRepoTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new UserRequestModel()
            {
                LocationId = locationId,
                LastName = "Test",
                FirstName = "Category",
            };
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await UserRepo.CreateUserAsync(UserTest);
            Assert.Null(UserNew);
        }

        [Fact]
        public async Task UpdateUser_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var userid = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new UserRequestModel()
            {
                UserId = userid,
                LocationId = locationId,
                LastName = "Test",
                FirstName = "Category",
            };
            // create repo
            var userRepository = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await userRepository.CreateUserAsync(UserTest);

            var newlocationId = Guid.NewGuid().ToString();
            var UserTestNew = new UserRequestModel()
            {
                UserId = userid,
                LastName = "Test1",
                FirstName = "Category",
            };
            var Createupdate = await userRepository.UpdateUserAsync(userid,UserTestNew);
            Assert.Null(Createupdate);
        }

        [Fact]
        public async Task DisableUser_Success()
        {
            var dbContext = _fixture.Context;
            var LocationId = Guid.NewGuid().ToString();
            var Id = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            dbContext.Locations.Add(new Location() { LocationId = LocationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new UserRequestModel()
            {
                UserId = Id,
                LocationId = LocationId,
                LastName = "Test",
                FirstName = "Category",
            };
            // create repo
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await UserRepo.CreateUserAsync(UserTest);
            var result = await UserRepo.DisableUserAsync(Id);
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserById_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var id = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User { });
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new UserRequestModel()
            {
                UserId =id,
                LocationId = locationId,
                LastName = "Test1",
                FirstName = "Category",
            };
            var userRepository = new UserRepository(dbContext,mockUserManager.Object);
            var UserNew = await userRepository.CreateUserAsync(UserTest);
            var user = await userRepository.GetUserByIdAsync(id);
            Assert.True(UserTest.UserId.Equals(id));
            Assert.Null(user);
            Assert.IsNotType<UserDetailModel>(user);
        }
        [Fact]
        public async Task GetListUser_Succsess()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new UserRequestModel()
            {
                LocationId = locationId,
                LastName = "Test",
                FirstName = "Category",
            };
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await UserRepo.CreateUserAsync(UserTest);
            var param = new UserRequestParmas
            {
                locationId = locationId,
                sortCode = Enums.SortBy.ASC,

            };       
            var user = await UserRepo.GetListUserAsync(param);
            Assert.IsType<List<UserModel>>(user.Datas);
            Assert.True(user.TotalPage >= 0);
        }
    }
 }
