using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IAssignmentService
    {
        Task<AssignmentModel> CreateAssignmentAsync(AssignmentRequestModel assignmentRequestModel);
        Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel);
        Task<bool> ChangeStateAssignmentAsync(string id, StateAssignment state);
        Task<bool> DeleteAssignmentAsync(string id);
        Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(MyAssignmentRequestParams myAssignmentRequestParams);
        Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(AssignmentRequestParams assignmentRequestParams);
        Task<AssignmentDetailModel> GetAssignmentById(string id);
    }
}