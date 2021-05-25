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
        private readonly SqliteInMemoryFixture _fixture;

        public AssignmentServiceTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
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
            (ICollection<AssignmentModel> Datas, int totalpage, int totalitem) List = new (collection, totalP, totali);
            mockAssignRepo.Setup(x => x.GetListAssignmentAsync(It.IsAny<AssignmentRequestParams>())).ReturnsAsync(List);
            var assign = new AssignmentService(mockAssignRepo.Object);
            var assignmentrequest = new AssignmentRequestParams();
            var result = await assign.GetListAssignmentAsync(assignmentrequest);
            Assert.NotNull(result.Datas);

        }
    }
}
