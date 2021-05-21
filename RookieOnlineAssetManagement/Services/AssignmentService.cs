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
            return await _assignmentRepository.CreateAssignmentAsync(assignmentRequestModel);
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            return await _assignmentRepository.UpdateAssignmentAsync(id, assignmentRequestModel);
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            return await _assignmentRepository.DeleteAssignmentAsync(id);
        }
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(AssignmentRequestParams assignmentRequestParams)
        {
            return await _assignmentRepository.GetListAssignmentAsync(assignmentRequestParams);
        }
        public async Task<AssignmentDetailModel> GetAssignmentById(string id)
        {
            return await _assignmentRepository.GetAssignmentById(id);
        }
    }
}
