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
    public class ReturnRequestServiceTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public ReturnRequestServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task GetListReturnRquest_Success()
        {
            var mockRequestRepo = new Mock<IReturnRequestRepository>();
            ReturnRequestModel Model = new ReturnRequestModel();
            List<ReturnRequestModel> collection = new List<ReturnRequestModel>();
            int totalP = 0;
            (ICollection<ReturnRequestModel> Datas, int totalpage) List = new(collection, totalP);
            mockRequestRepo.Setup(x => x.GetListReturnRequestAsync(It.IsAny<ReturnRequestParams>())).ReturnsAsync(List);
            var request = new ReturnRequestService(mockRequestRepo.Object);
            var requestParams = new ReturnRequestParams();
            var result = await request.GetListReturnRequestAsync(requestParams);
            Assert.NotNull(result.Datas);
        }

        [Fact]
        public async Task ChangeStateAccept_Success()
        {
            bool state = true;
            var assignId = Guid.NewGuid().ToString();
            var acceoptId = Guid.NewGuid().ToString();
            var mockRequestRepo = new Mock<IReturnRequestRepository>();
            ReturnRequestModel Model = new ReturnRequestModel();
            mockRequestRepo.Setup(x => x.ChangeStateAsync(It.IsAny<bool>(), It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(true);
            var request = new ReturnRequestService(mockRequestRepo.Object);
            var result = await request.ChangeStateAsync(state, assignId, acceoptId);
            Assert.True(result);
        }

        [Fact]
        public async Task ChangeStateCancel_Success()
        {
            bool state = false;
            var assignId = Guid.NewGuid().ToString();
            var acceoptId = Guid.NewGuid().ToString();
            var mockRequestRepo = new Mock<IReturnRequestRepository>();
            ReturnRequestModel Model = new ReturnRequestModel();
            mockRequestRepo.Setup(x => x.ChangeStateAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            var request = new ReturnRequestService(mockRequestRepo.Object);
            var result = await request.ChangeStateAsync(state, assignId, acceoptId);
            Assert.True(result);
        }

    }
}
