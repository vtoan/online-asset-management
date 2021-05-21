using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task<AssignmentModel> CreateAssignmentAsync(AssignmentRequestModel assignmentRequestModel)
        {
            var checkassigneddate = CheckDateGreaterThan(DateTime.Now, assignmentRequestModel.AssignedDate.Value);
            if (checkassigneddate == false)
            {
                throw new Exception("Assigned Date is smaller than Today");
            }
            return await _assignmentRepository.CreateAssignmentAsync(assignmentRequestModel);
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            return await _assignmentRepository.UpdateAssignmentAsync(id, assignmentRequestModel);
        }
        public async Task<bool> ChangeStateAssignmentAsync(string id, StateAssignment state)
        {
            return await _assignmentRepository.ChangeStateAssignmentAsync(id, state);
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            return await _assignmentRepository.DeleteAssignmentAsync(id);
        }
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(StateAssignment[] StateAssignments, string AssignedDateAssignment, string query, SortBy AssetId, SortBy AssetName, SortBy AssignedTo, SortBy AssignedBy, SortBy AssignedDate, SortBy State, int page, int pageSize)
        {
            return await _assignmentRepository.GetListAssignmentAsync(StateAssignments, AssignedDateAssignment, query, AssetId, AssetName, AssignedTo, AssignedBy, AssignedDate, State, page, pageSize);
        }
        public async Task<AssetDetailModel> GetAssignmentById(string id)
        {
            return await _assignmentRepository.GetAssignmentById(id);
        }

        public bool CheckDateGreaterThan(DateTime SmallDate, DateTime BigDate)
        {
            if (SmallDate > BigDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
