using Microsoft.AspNetCore.Mvc;
using Moq;
using RookieOnlineAssetManagement.Controllers;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using RookieOnlineAssetManagement.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Controller
{
    public class UserControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public UserControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            var mockUserSer = new Mock<IUserService>();
            var locationId = Guid.NewGuid().ToString();
            var UserTest = new UserRequestModel()
            {
                LocationId = locationId,
                LastName = "Test",
                FirstName = "Category",
            };
            var usermodel = new UserModel();
            mockUserSer.Setup(m => m.CreateUserAsync(It.IsAny<UserRequestModel>())).ReturnsAsync(usermodel);
            var Usercontr = new UsersController(mockUserSer.Object);
            var result = await Usercontr.CreateAsync(UserTest);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateUser_Success()
        {
            var mockUserSer = new Mock<IUserService>();
            var locationId = Guid.NewGuid().ToString();
            var UserTest = new UserRequestModel
            {
                LocationId=locationId,
                UserId = Guid.NewGuid().ToString(),
                FirstName = "Demo",
                LastName = "User",
                DateOfBirth = new DateTime(1999, 04, 27),
                JoinedDate = new DateTime(2021, 04, 27),
            };
            var usermodel = new UserModel();
            mockUserSer.Setup(x => x.UpdateUserAsync(It.IsAny<string>(), It.IsAny<UserRequestModel>())).ReturnsAsync(usermodel);
            var assetContr = new UsersController(mockUserSer.Object);
            var result = await assetContr.UpdateAsync(UserTest.UserId, UserTest);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DisableUser_Success()
        {
            var mockUserSer = new Mock<IUserService>();
            string UserId = Guid.NewGuid().ToString();
            mockUserSer.Setup(x => x.DisableUserAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assetContr = new UsersController(mockUserSer.Object);
            var result = await assetContr.DisableAsync(UserId);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetUser_Success()
        {
            var mockUserSer = new Mock<IUserService>();
            string UserId = Guid.NewGuid().ToString();
            var UserModel = new UserDetailModel();
            mockUserSer.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(UserModel);
            var assetContr = new UsersController(mockUserSer.Object);
            var result = await assetContr.GetAsync(UserId);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}
