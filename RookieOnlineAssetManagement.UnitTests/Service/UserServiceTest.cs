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
    public class UserServiceTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public UserServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }

        [Fact]
        public async Task CreateUser_Success()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            var UserTest = new UserRequestModel
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "Demo",
                LastName = "User",
                DateOfBirth = new DateTime(1999,04,27),
                JoinedDate = new DateTime(2021, 04, 27),
            };
            mockUserRepo.Setup(m => m.CreateUserAsync(It.IsAny<UserRequestModel>())).ReturnsAsync(UserTest);
            var userService = new UserService(mockUserRepo.Object);
            var result = await userService.CreateUserAsync(UserTest);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserById_Success()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            string Userid = Guid.NewGuid().ToString();
            var UserMosel = new UserDetailModel();
            mockUserRepo.Setup(x => x.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(UserMosel);
            var assetSer = new UserService(mockUserRepo.Object);
            var result = await assetSer.GetUserByIdAsync(Userid);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DisableUser_Success()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            string Userid = Guid.NewGuid().ToString();
            mockUserRepo.Setup(x => x.DisableUserAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assetSer = new UserService(mockUserRepo.Object);
            var result = await assetSer.DisableUserAsync(Userid);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUser_Success()
        {
            var mockUserRepo = new Mock<IUserRepository>();
            var UserTest = new UserRequestModel
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = "Demo",
                LastName = "User",
                DateOfBirth = new DateTime(1999, 04, 27),
                JoinedDate = new DateTime(2021, 04, 27),
            };
            mockUserRepo.Setup(x => x.UpdateUserAsync(It.IsAny<string>(), It.IsAny<UserRequestModel>())).ReturnsAsync(UserTest);
            var assetSer = new UserService(mockUserRepo.Object);
            var result = await assetSer.UpdateUserAsync(UserTest.UserId, UserTest);
            Assert.NotNull(result);
        }
    }
}
