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
        Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(string userid, string locationid, SortBy? AssetIdSort, SortBy? AssetNameSort, SortBy? CategoryNameSort, SortBy? AssignedDateSort, SortBy? StateSort);
        Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(StateAssignment[] StateAssignments, string AssignedDateAssignment, string query, SortBy AssetId, SortBy AssetName, SortBy AssignedTo, SortBy AssignedBy, SortBy AssignedDate, SortBy State, int page, int pagesize);
        Task<AssetDetailModel> GetAssignmentById(string id);
    }
}