using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class ReturnRequestRepository : BaseRepository, IReturnRequestRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReturnRequestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ReturnRequestModel> CreateReturnRequestAsync(string assignmentId, string requestedUserId)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == assignmentId);
            if (assignment == null)
            {
                throw new Exception("Repository | Have not this assignment");
            }
            if (!assignment.State.Equals((int)StateAssignment.Accepted))
            {
                throw new Exception("Repository | State is not valid");
            }
            var requestedUser = await _dbContext.Users.FindAsync(requestedUserId);
            if (requestedUser == null)
            {
                throw new Exception("Repository | Request User not exists");
            }
            var returnRequest = new ReturnRequest
            {
                AssignmentId = assignment.AssignmentId,
                RequestUserId = requestedUser.Id,
                RequestBy = requestedUser.UserName,
                State = false
            };
            var create = _dbContext.ReturnRequests.Add(returnRequest);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                var returnRequestModel = new ReturnRequestModel
                {
                    AssignmentId = create.Entity.AssignmentId,
                    AssetId = create.Entity.Assignment.AssetId,
                    AssetName = create.Entity.Assignment.AssetName,
                    AcceptedUserId = create.Entity.AcceptedUserId,
                    AcceptedBy = create.Entity.AcceptedBy,
                    RequestUserId = create.Entity.RequestUserId,
                    RequestBy = create.Entity.RequestBy,
                    ReturnedDate = create.Entity.ReturnDate,
                    State = create.Entity.State
                };
                return returnRequestModel;
            }
            throw new Exception("Repository | Create return request fail");
        }
        public async Task<bool> ChangeStateAsync(bool accept, string assignmentId, string acceptedUserId)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == assignmentId);
            if (assignment == null)
            {
                throw new Exception("Repository | Have not this assignment");
            }
            if (assignment.State != (int)StateAssignment.Accepted)
            {
                throw new Exception("Repository | State must be accepted");
            }
            var returnRequest = await _dbContext.ReturnRequests.FirstOrDefaultAsync(x => x.AssignmentId == assignmentId);
            if (returnRequest.State == true)
            {
                throw new Exception("Repository | State is completed");
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (accept == true)
                {
                    var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
                    var acceptedUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == acceptedUserId);
                    if (acceptedUser == null)
                    {
                        throw new Exception("Repository | Have not this accept user");
                    }
                    asset.State = (short)StateAsset.Avaiable;
                    assignment.State = (int)StateAssignment.Completed;
                    returnRequest.State = true;
                    returnRequest.ReturnDate = DateTime.Now;
                    returnRequest.AcceptedUserId = acceptedUser.Id;
                    returnRequest.AcceptedBy = acceptedUser.UserName;
                }
                else
                {
                    var deleted = _dbContext.ReturnRequests.Remove(returnRequest);
                }
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    transaction.Commit();
                    return true;
                }
                throw new Exception("Repository | Change state return request fail");
            }
            catch
            {
                throw new Exception("Repository | Change state return request fail");
            }
        }
        public async Task<(ICollection<ReturnRequestModel> Datas, int TotalPage, int TotalItem)> GetListReturnRequestAsync(ReturnRequestParams returnRequestParams)
        {
            await this.LocationIsExist(_dbContext, returnRequestParams.LocationId);
            //filter
            var queryable = _dbContext.ReturnRequests.Include(x => x.Assignment).Where(x => x.Assignment.LocationId == returnRequestParams.LocationId);
            if (returnRequestParams.StateReturnRequests != null)
            {
                //var stateNum = Convert.ToInt32(returnRequestParams.StateReturnRequests);
                queryable = queryable.Where(x => returnRequestParams.StateReturnRequests.Contains(x.State));
            }
            if (!string.IsNullOrEmpty(returnRequestParams.ReturnedDate))
            {
                var Date = Convert.ToDateTime(returnRequestParams.ReturnedDate);
                queryable = queryable.Where(x => Date.Day == x.ReturnDate.Value.Day && Date.Month == x.ReturnDate.Value.Month && Date.Year == x.ReturnDate.Value.Year);
            }
            if (!string.IsNullOrEmpty(returnRequestParams.Query))
            {
                queryable = queryable.Where(x => x.Assignment.AssetId.Contains(returnRequestParams.Query) || x.Assignment.AssetName.Contains(returnRequestParams.Query) || x.RequestBy.Contains(returnRequestParams.Query));
            }
            //sort
            var q = queryable.Select(x => new ReturnRequestModel
            {
                AssignmentId = x.AssignmentId,
                AssetId = x.Assignment.AssetId,
                AssetName = x.Assignment.AssetName,
                RequestUserId = x.RequestUserId,
                RequestBy = x.RequestBy,
                AssignedDate = x.Assignment.AssignedDate,
                AcceptedUserId = x.AcceptedUserId,
                AcceptedBy = x.AcceptedBy,
                ReturnedDate = x.ReturnDate,
                State = x.State
            });
            q = this.SortData<ReturnRequestModel, ReturnRequestParams>(q, returnRequestParams);
            //paging
            var result = Paging<ReturnRequestModel>(q, returnRequestParams.PageSize, returnRequestParams.Page);
            var list = await result.Sources.ToListAsync();
            return (list, result.TotalPage, result.TotalItem);
        }
    }
}
