using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class ReturnRequestRepository: IReturnRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReturnRequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ReturnRequestModel> CreateReturnRequestAsync(string assignmentId,string requestedUserId)
        {
            return null;
        }
        public async Task<bool> ChangeStateAsync(bool state, string assignmentId)
        {
            return true;
        }
        public async Task<ICollection<ReturnRequestModel>> GetListReturnRequestAsync(ReturnRequestParams returnRequestParams)
        {
            return null;
        }
    }
}
