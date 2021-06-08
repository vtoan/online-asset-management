using Microsoft.AspNetCore.Identity;
using Moq;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
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
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
                Type = Enums.TypeUser.STAFF
            };
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await UserRepo.CreateUserAsync(UserTest);
            Assert.NotNull(UserNew);
        }

        [Fact]
        public async Task UpdateUser_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            //
            var mockLocation = new Location() { LocationId = locationId, LocationName = "HCM" };
            var mockUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                StaffCode = "123",
                UserName = "demod",
                LocationId = mockLocation.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
            };
            var mockRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "amdin", NormalizedName = "ADMIN" };
            var mockUserRole = new IdentityUserRole<string>() { UserId = mockUser.Id, RoleId = mockRole.Id };
            dbContext.Add<User>(mockUser);
            dbContext.Roles.Add(mockRole);
            dbContext.UserRoles.Add(mockUserRole);
            dbContext.Locations.Add(mockLocation);
            await dbContext.SaveChangesAsync();

            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);

            var UserTestNew = new UserRequestModel()
            {
                UserId = mockUser.Id,
                LocationId = mockLocation.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-22"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
                Type = Enums.TypeUser.ADMIN
            };
            var Createupdate = await UserRepo.UpdateUserAsync(UserTestNew.UserId, UserTestNew);
            Assert.NotNull(Createupdate);
        }

        [Fact]
        public async Task DisableUser_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            //
            var mockLocation = new Location() { LocationId = locationId, LocationName = "HCM" };
            var mockUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                StaffCode = "123",
                UserName = "demod",
                LocationId = mockLocation.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
            };
            var mockRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "ADMIN", NormalizedName = "admin" };
            var mockUserRole = new IdentityUserRole<string>() { UserId = mockUser.Id, RoleId = mockRole.Id };
            dbContext.Add<User>(mockUser);
            dbContext.Roles.Add(mockRole);
            dbContext.UserRoles.Add(mockUserRole);
            dbContext.Locations.Add(mockLocation);
            await dbContext.SaveChangesAsync();
            //
            mockUserManager.Setup(x => x.SetLockoutEnabledAsync(It.IsAny<User>(), It.IsAny<bool>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.SetLockoutEndDateAsync(It.IsAny<User>(), It.IsAny<DateTime>())).ReturnsAsync(IdentityResult.Success);
            //
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            // var UserNew = await UserRepo.CreateUserAsync(UserTest);
            var result = await UserRepo.DisableUserAsync(mockUser.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task GetUserById_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var id = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            //
            var mockRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "ADMIN", NormalizedName = "admin" };
            var mockUserRole = new IdentityUserRole<string>() { UserId = id, RoleId = mockRole.Id };
            var mockLocation = new Location() { LocationId = locationId, LocationName = "HCM" };
            var mockUser = new User()
            {
                Id = id,
                StaffCode = "123",
                UserName = "demod",
                LocationId = mockLocation.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
            };
            //
            dbContext.Add<User>(mockUser);
            dbContext.Roles.Add(mockRole);
            dbContext.UserRoles.Add(mockUserRole);
            dbContext.Locations.Add(mockLocation);
            await dbContext.SaveChangesAsync();
            var userRepository = new UserRepository(dbContext, mockUserManager.Object);
            var user = await userRepository.GetUserByIdAsync(id);
            Assert.NotNull(user);
        }
        [Fact]
        public async Task GetListUser_Succsess()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            //
            var mockLocation = new Location() { LocationId = locationId, LocationName = "HCM" };
            var mockUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                StaffCode = "123",
                UserName = "demod",
                LocationId = mockLocation.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
            };
            var mockRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "ADMIN", NormalizedName = "admin" };
            var mockUserRole = new IdentityUserRole<string>() { UserId = mockUser.Id, RoleId = mockRole.Id };
            dbContext.Add<User>(mockUser);
            dbContext.Roles.Add(mockRole);
            dbContext.UserRoles.Add(mockUserRole);
            dbContext.Locations.Add(mockLocation);
            await dbContext.SaveChangesAsync();
            //
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var param = new UserRequestParmas
            {
                LocationId = locationId,
                SortCode = Enums.SortBy.ASC,
            };
            var user = await UserRepo.GetListUserAsync(param);
            Assert.IsType<List<UserModel>>(user.Datas);
            Assert.True(user.TotalPage >= 0);
        }
    }
}
