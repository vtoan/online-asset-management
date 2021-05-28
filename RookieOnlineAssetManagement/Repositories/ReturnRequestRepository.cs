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
    public class ReturnRequestRepository :  BaseRepository, IReturnRequestRepository
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
            if(returnRequest.State==true)
            {
                throw new Exception("Repository | State is completed");
            }
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (accept == true)
                {
                    var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
                    var acceptedUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == acceptedUserId);
                    if (acceptedUser == null)
                    {
                        throw new Exception("Have not this accept user");
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
        public async Task<(ICollection<ReturnRequestModel>Datas, int TotalPage)> GetListReturnRequestAsync(ReturnRequestParams returnRequestParams)
        {
            var queryable = _dbContext.ReturnRequests.Include(x => x.Assignment).Where(x => x.Assignment.LocationId == returnRequestParams.LocationId);
            if(returnRequestParams.StateReturnRequests != null)
            {
                //var stateNum = Convert.ToInt32(returnRequestParams.StateReturnRequests);
                queryable = queryable.Where(x => returnRequestParams.StateReturnRequests.Contains(x.State));
            }
            if(!string.IsNullOrEmpty(returnRequestParams.ReturnedDate))
            {
                var Date = Convert.ToDateTime(returnRequestParams.ReturnedDate);
                queryable = queryable.Where(x => Date.Day == x.ReturnDate.Value.Day && Date.Month == x.ReturnDate.Value.Month && Date.Year == x.ReturnDate.Value.Year);
            }
            if(!string.IsNullOrEmpty(returnRequestParams.Query) )
            {
                queryable = queryable.Where(x => x.Assignment.AssetId.Contains(returnRequestParams.Query) || x.Assignment.AssetName.Contains(returnRequestParams.Query) || x.RequestBy.Contains(returnRequestParams.Query));
            }
            if(returnRequestParams.SortAssetId.HasValue)
            {
                if(returnRequestParams.SortAssetId.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Assignment.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.Assignment.AssetId);
            }
            else if(returnRequestParams.SortAssetName.HasValue)
            {
                if(returnRequestParams.SortAssetName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Assignment.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.Assignment.AssetName);
            }
            else if(returnRequestParams.SortRequestedBy.HasValue)
            {
                if(returnRequestParams.SortRequestedBy.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.RequestBy);
                else
                    queryable = queryable.OrderByDescending(x => x.RequestBy);
            }
            else if(returnRequestParams.SortAssignedDate.HasValue)
            {
                if (returnRequestParams.SortAssignedDate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Assignment.AssignedDate);
                else
                    queryable = queryable.OrderByDescending(x => x.Assignment.AssignedDate);
            }
            else if(returnRequestParams.SortAcceptedBy.HasValue)
            {
                if (returnRequestParams.SortAcceptedBy.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AcceptedBy);
                else
                    queryable = queryable.OrderByDescending(x => x.AcceptedBy);
            }
            else if(returnRequestParams.SortReturnedDate.HasValue)
            {
                if (returnRequestParams.SortReturnedDate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.ReturnDate);
                else
                    queryable = queryable.OrderByDescending(x => x.ReturnDate);
            }
            else if (returnRequestParams.SortState.HasValue)
            {
                if (returnRequestParams.SortState.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.State);
                else
                    queryable = queryable.OrderByDescending(x => x.State);
            }
            var result = Paging<ReturnRequest>(queryable, returnRequestParams.PageSize, returnRequestParams.Page);
            var list = await result.Sources.Select(x => new ReturnRequestModel
            {
                AssetId = x.Assignment.AssetId,
                AssetName = x.Assignment.AssetName,
                RequestBy = x.RequestBy,
                AssignedDate = x.Assignment.AssignedDate,
                AcceptedBy = x.AcceptedBy,
                ReturnedDate = x.ReturnDate,
                State = x.State
            }).ToListAsync();
            return (list,result.TotalPage);
        }
    }
}
