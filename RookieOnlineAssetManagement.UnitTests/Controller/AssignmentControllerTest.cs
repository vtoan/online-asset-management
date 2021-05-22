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
    public class AssignmentControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        [Fact]
        public async Task Create_Success()
        {
            var mockAssignmentSer = new Mock<IAssignmentService>();
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                AssetId = "LD100040",
                AdminId = Guid.NewGuid().ToString(),
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = Guid.NewGuid().ToString(),
            };
            var assignmentmodel = new AssignmentModel();
            mockAssignmentSer.Setup(m => m.CreateAssignmentAsync(It.IsAny<AssignmentRequestModel>())).ReturnsAsync(assignmentmodel);
            var assignmentcontr = new AssignmentsController(mockAssignmentSer.Object);
            var result = await assignmentcontr.CreateAsync(assignmentrequsetmodel);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Update_Success()
        {
            var mockAssignmentSer = new Mock<IAssignmentService>();
            var assignmentrequsetmodel = new AssignmentRequestModel
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                AssetId = "LD100040",
                AdminId = Guid.NewGuid().ToString(),
                AssignedDate = DateTime.Now,
                Note = "test",
                LocationId = Guid.NewGuid().ToString(),
            };
            var assignmentmodel = new AssignmentModel();
            mockAssignmentSer.Setup(x => x.UpdateAssignmentAsync(It.IsAny<string>(), It.IsAny<AssignmentRequestModel>())).ReturnsAsync(assignmentmodel);
            var assignmentcontr = new AssignmentsController(mockAssignmentSer.Object);
            var result = await assignmentcontr.UpdateAsync(assignmentrequsetmodel.AssetId, assignmentrequsetmodel);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_Success()
        {
            var mockAssignmentSer = new Mock<IAssignmentService>();
            string assignmentid = Guid.NewGuid().ToString();
            mockAssignmentSer.Setup(x => x.DeleteAssignmentAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assignmentcontr = new AssignmentsController(mockAssignmentSer.Object);
            var result = await assignmentcontr.DeleteAsync(assignmentid);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
