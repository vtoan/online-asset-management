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
    public class AssignmentServiceTest : IClassFixture<SqliteInMemoryFixture>
    {
        public AssignmentServiceTest()
        {

        }
        [Fact]
        public async Task CreateAssignment_Success()
        {
            var mockAssignmentRepo = new Mock<IAssignmentRepository>();
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
            mockAssignmentRepo.Setup(m => m.CreateAssignmentAsync(It.IsAny<AssignmentRequestModel>())).ReturnsAsync(assignmentmodel);
            var assignmentSer = new AssignmentService(mockAssignmentRepo.Object);
            var result = await assignmentSer.CreateAssignmentAsync(assignmentrequsetmodel);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UpdateAssignment_Success()
        {
            var mockAssignmentRepo = new Mock<IAssignmentRepository>();
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
            mockAssignmentRepo.Setup(x => x.UpdateAssignmentAsync(It.IsAny<string>(), It.IsAny<AssignmentRequestModel>())).ReturnsAsync(assignmentmodel);
            var assignmentSer = new AssignmentService(mockAssignmentRepo.Object);
            var result = await assignmentSer.UpdateAssignmentAsync(assignmentrequsetmodel.AssetId, assignmentrequsetmodel);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteAssignment_Success()
        {
            var mockAssignmentRepo = new Mock<IAssignmentRepository>();
            string assignmentId = Guid.NewGuid().ToString();
            mockAssignmentRepo.Setup(x => x.DeleteAssignmentAsync(It.IsAny<string>())).ReturnsAsync(true);
            var assignmentSer = new AssignmentService(mockAssignmentRepo.Object);
            var result = await assignmentSer.DeleteAssignmentAsync(assignmentId);
            Assert.True(result);
        }
    }
}
