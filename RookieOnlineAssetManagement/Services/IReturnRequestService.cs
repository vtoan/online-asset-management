using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IReturnRequestService
    {
        Task<ReturnRequestModel> CreateReturnRequestAsync(string assignmentId, string requestedUserId);
        Task<bool> ChangeStateAsync(bool state, string assignmentId);
        Task<ICollection<ReturnRequestModel>> GetListReturnRequestAsync(ReturnRequestParams returnRequestParams);
    }
}