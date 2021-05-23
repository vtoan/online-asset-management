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
        public async Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(string userid, string locationid, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort, SortBy? AssignedDateSort, SortBy? StateSort)
        {
            return await _assignmentRepository.GetListMyAssignmentAsync(userid, locationid, AssetIdSort, AssetNameSort, CategoryNameSort, AssignedDateSort, StateSort);
        }
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(AssignmentRequestParams assignmentRequestParams)
        {
            return await _assignmentRepository.GetListAssignmentAsync(assignmentRequestParams);
        }
        public async Task<AssignmentDetailModel> GetAssignmentById(string id)
        {
            return await _assignmentRepository.GetAssignmentById(id);
        }

        public bool CheckDateGreaterThan(DateTime SmallDate, DateTime BigDate)
        {
            if (SmallDate.Date > BigDate.Date)
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
