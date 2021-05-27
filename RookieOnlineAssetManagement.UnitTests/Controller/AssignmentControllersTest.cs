using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RookieOnlineAssetManagement.Controllers;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RookieOnlineAssetManagement.UnitTests.Controller
{
   public class AssignmentControllersTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public AssignmentControllersTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task GetAssignmentById_Success()
        {
            var mockAssignRepo = new Mock<IAssignmentService>();
            AssignmentDetailModel Model = new AssignmentDetailModel();
            mockAssignRepo.Setup(x => x.GetAssignmentById(It.IsAny<string>())).ReturnsAsync(Model);
            var assigntSer = new AssignmentsController(mockAssignRepo.Object);
            var result = await assigntSer.GetAssignmentById(Model.AssignmentId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetListAssignment_success()
        {
            var HttpContext = new DefaultHttpContext();
            HttpContext.Request.Headers["total-pages"] = "0";
            HttpContext.Request.Headers["total-item"] = "0";
            var mokService = new Mock<IAssignmentService>();
            List<AssignmentModel> collection = new List<AssignmentModel>();
            int totalP = 0;
            int totali = 0;
            (ICollection<AssignmentModel> Datas, int totalpage, int totalitem) List = new(collection, totalP, totali);
            mokService.Setup(x => x.GetListAssignmentAsync(It.IsAny<AssignmentRequestParams>())).ReturnsAsync(List);
            AssignmentModel model = new AssignmentModel();
            var assignmentrequest = new AssignmentRequestParams();
            var controller = new AssignmentsController(mokService.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = HttpContext,
                }
            };
            var resutl =await controller.GetListAsync(assignmentrequest);

            Assert.NotNull(resutl);
            Assert.IsType<OkObjectResult>(resutl);
        }
    }
}
