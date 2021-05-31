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
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            dbContext.Locations.Add(new Location() { LocationId = locationId, LocationName = "HCM" });
            await dbContext.SaveChangesAsync();
            var UserTest = new User()
            {
                LocationId = locationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-20"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
                StaffCode = "SD0001"
            };
            var Role = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Staff",
                NormalizedName = TypeUser.STAFF.ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var UserRepo = new UserRepository(dbContext, mockUserManager.Object);
            var createuser = dbContext.Users.Add(UserTest);
            var createrole = dbContext.Roles.Add(Role);
            var UserRole = new IdentityUserRole<string>()
            {
                RoleId = createrole.Entity.Id,
                UserId = createuser.Entity.Id
            };
            var createuserrol = dbContext.UserRoles.Add(UserRole);

            var UserTestNew = new UserRequestModel()
            {
                UserId = createuser.Entity.Id,
                LocationId = createuser.Entity.LocationId,
                LastName = "Test",
                FirstName = "Category",
                DateOfBirth = DateTime.Parse("1999-10-22"),
                Gender = true,
                JoinedDate = DateTime.Parse("2020-6-30"),
                Type = Enums.TypeUser.STAFF
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
            var result = await UserRepo.DisableUserAsync(UserNew.UserId);
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserById_Success()
        {
            var dbContext = _fixture.Context;
            var locationId = Guid.NewGuid().ToString();
            var id = Guid.NewGuid().ToString();
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new User { });
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
                Type = TypeUser.STAFF
            };
            var userRepository = new UserRepository(dbContext, mockUserManager.Object);
            var UserNew = await userRepository.CreateUserAsync(UserTest);
            var user = await userRepository.GetUserByIdAsync(UserNew.UserId);
            //Assert.True(UserTest.UserId.Equals(id));
            Assert.NotNull(user);
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
                LocationId = locationId,
                SortCode = Enums.SortBy.ASC,
            };       
            var user = await UserRepo.GetListUserAsync(param);
            Assert.IsType<List<UserModel>>(user.Datas);
            Assert.True(user.TotalPage >= 0);
        }
    }
 }
