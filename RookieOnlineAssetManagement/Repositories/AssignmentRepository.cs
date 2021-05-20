using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class AssignmentRepository: IAssignmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AssignmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AssignmentModel> CreateAssignmentAsync(AssignmentRequestModel assignmentRequestModel)
        {
            return null;
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            return null;
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            return true;
        }
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(StateAssignment[] StateAssignments, string AssignedDateAssignment, string query, SortBy AssetId, SortBy AssetName, SortBy AssignedTo, SortBy AssignedBy, SortBy AssignedDate, SortBy State, int page, int pageSize)
        {
            return (null, 0, 0);
        }
        public async Task<AssetDetailModel> GetAssignmentById(string id)
        {
            return null;
        }
    }
}
