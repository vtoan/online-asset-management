using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RookieOnlineAssetManagement.Controllers;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Controller
{
    public class ReturnRequestControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public ReturnRequestControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task Create_Success()
        {
            var mokService = new Mock<IReturnRequestService>();
            Mock<ISession> sessionMock = new Mock<ISession>();
            ReturnRequestModel returnRequestModel = new ReturnRequestModel();
            mokService.Setup(x => x.CreateReturnRequestAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(returnRequestModel);

            var controller = new ReturnRequestsController(mokService.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;

            var result = await controller.CreateAsync(Guid.NewGuid().ToString());
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ChangeStateAccept_Success()
        {
            var mokService = new Mock<IReturnRequestService>();
            Mock<ISession> sessionMock = new Mock<ISession>();
            mokService.Setup(x => x.ChangeStateAsync(It.IsAny<bool>(),It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            var controller = new ReturnRequestsController(mokService.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;

            var result = await controller.ChangeStateAcceptAsync(Guid.NewGuid().ToString());
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task ChangeStateCancel_Success()
        {
            var mokService = new Mock<IReturnRequestService>();
            Mock<ISession> sessionMock = new Mock<ISession>();
            mokService.Setup(x => x.ChangeStateAsync(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            var controller = new ReturnRequestsController(mokService.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Session = sessionMock.Object;

            var result = await controller.ChangeStateCancelAsync(Guid.NewGuid().ToString());
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task GetList_Success()
        {
            var HttpContext = new DefaultHttpContext();
            //Mock<ISession> sessionMock = new Mock<ISession>();
            HttpContext.Request.Headers["total-pages"] = "0";
            HttpContext.Request.Headers["total-item"] = "0";
            var mokService = new Mock<IReturnRequestService>();
            List<ReturnRequestModel> collection = new List<ReturnRequestModel>();
            int totalP = 0;
            int totalI = 0;
            (ICollection<ReturnRequestModel> Datas, int totalpage, int totalitem) List = new(collection, totalP, totalI);
            mokService.Setup(x => x.GetListReturnRequestAsync(It.IsAny<ReturnRequestParams>())).ReturnsAsync(List);
            ReturnRequestModel model = new ReturnRequestModel();
            var Request = new ReturnRequestParams();
            var controller = new ReturnRequestsController(mokService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = HttpContext,
                }
            };
            //controller.ControllerContext.HttpContext = new DefaultHttpContext();
            //controller.ControllerContext.HttpContext.Session = sessionMock.Object;
            var result = await controller.GetListAsync(Request);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}

