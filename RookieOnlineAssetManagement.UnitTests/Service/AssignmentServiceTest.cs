using Moq;
using RookieOnlineAssetManagement.Enums;
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
        private readonly SqliteInMemoryFixture _fixture;

        public AssignmentServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
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
        public async Task ChangeStateAssignment_Success()
        {
            var mockAssignmentRepo = new Mock<IAssignmentRepository>();
            mockAssignmentRepo.Setup(x => x.ChangeStateAssignmentAsync(It.IsAny<string>(), It.IsAny<StateAssignment>())).ReturnsAsync(true);
            var assignmentSer = new AssignmentService(mockAssignmentRepo.Object);
            var result = await assignmentSer.ChangeStateAssignmentAsync(Guid.NewGuid().ToString(), StateAssignment.Accepted);
            Assert.True(result);
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
        [Fact]
        public async Task GetAssignmentById_Success()
        {
            var mockAssignRepo = new Mock<IAssignmentRepository>();
            AssignmentDetailModel Model = new AssignmentDetailModel();
            mockAssignRepo.Setup(x => x.GetAssignmentById(It.IsAny<string>())).ReturnsAsync(Model);
            var assetSer = new AssignmentService(mockAssignRepo.Object);
            var result = await assetSer.GetAssignmentById(Model.AssignmentId);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetAssignment_Success()
        {
            var mockAssignRepo = new Mock<IAssignmentRepository>();
            AssignmentModel Model = new AssignmentModel();
            List<AssignmentModel> collection = new List<AssignmentModel>();
            int totalP = 0;
            int totali = 0;
            (ICollection<AssignmentModel> Datas, int totalpage, int totalitem) List = new(collection, totalP, totali);
            mockAssignRepo.Setup(x => x.GetListAssignmentAsync(It.IsAny<AssignmentRequestParams>())).ReturnsAsync(List);
            var assign = new AssignmentService(mockAssignRepo.Object);
            var assignmentrequest = new AssignmentRequestParams();
            var result = await assign.GetListAssignmentAsync(assignmentrequest);
            Assert.NotNull(result.Datas);

        }
    }
}
