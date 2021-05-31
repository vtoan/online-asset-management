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
    public class AssignmentRepository: BaseRepository,IAssignmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AssignmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AssignmentModel> CreateAssignmentAsync(AssignmentRequestModel assignmentRequestModel)
        {
            var returnrequest = await _dbContext.ReturnRequests.FirstOrDefaultAsync(x => x.AssignmentId == assignmentRequestModel.AssignmentId);
            if (returnrequest != null)
            {
                throw new Exception("Repository | Return request have exists");
            }
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assignmentRequestModel.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
            if (AssignToUserId == null)
            {
                throw new Exception("Repository | Have not this Assign To User");
            }
            var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
            if(AssignByAdminId==null)
            {
                throw new Exception("Repository | Have not this Assign By Admin");
            }
            var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
            if (Asset == null)
            {
                throw new Exception("Repository | Have not this asset");
            }
            var assignment = new Assignment
            {
                AssignmentId = Guid.NewGuid().ToString(),
                UserId = assignmentRequestModel.UserId,
                AssignTo = AssignToUserId.UserName,
                AssetId = assignmentRequestModel.AssetId,
                AssetName = Asset.AssetName,
                AdminId = assignmentRequestModel.AdminId,
                AssignBy = AssignByAdminId.UserName,
                AssignedDate = assignmentRequestModel.AssignedDate,
                Note = assignmentRequestModel.Note,
                LocationId = assignmentRequestModel.LocationId,
                State = (int)StateAssignment.WaitingForAcceptance,
            };
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                Asset.State = (short)StateAsset.Assigned;

                var create = _dbContext.Assignments.Add(assignment);
                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    var assignmentmodel = new AssignmentModel
                    {
                        AssignmentId = create.Entity.AssignmentId,
                        UserId = create.Entity.UserId,
                        AssignedTo = create.Entity.AssignTo,
                        AssetId = create.Entity.AssetId,
                        AssetName = create.Entity.AssetName,
                        AdminId = create.Entity.AdminId,
                        AssignedBy = create.Entity.AssignBy,
                        LocationId = create.Entity.LocationId,
                        AssignedDate = create.Entity.AssignedDate,
                        State = create.Entity.State
                    };
                    transaction.Commit();
                    return assignmentmodel;
                }
                else
                {
                    throw new Exception("Repository | Create assignment fail");
                }
            }
            catch
            {
                throw new Exception("Repository | Create assignment fail");
            }
            
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assignmentRequestModel.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            if (!assignmentRequestModel.State.Equals((int)StateAssignment.WaitingForAcceptance))
            {
                throw new Exception("Repository | State must be waiting for acceptance");
            }

            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw new Exception("Repository | Have not this assignment");
            }


            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (assignment.UserId != assignmentRequestModel.UserId)
                {
                    var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
                    if (AssignToUserId == null)
                    {
                        throw new Exception("Repository | Have not this Assign To User");
                    }
                    assignment.UserId = assignmentRequestModel.UserId;
                    assignment.AssignTo = AssignToUserId.UserName;
                }
                if (assignment.AdminId != assignmentRequestModel.AdminId)
                {
                    var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
                    if (AssignByAdminId == null)
                    {
                        throw new Exception("Repository | Have not this Assign By Admin");
                    }
                    assignment.AdminId = assignmentRequestModel.AdminId;
                    assignment.AssignBy = AssignByAdminId.UserName;
                }
                if (assignment.AssetId != assignmentRequestModel.AssetId)
                {
                    var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
                    if (Asset == null)
                    {
                        throw new Exception("Repository | Have not this new asset");
                    }
                    var AssetOld = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
                    if (AssetOld == null)
                    {
                        throw new Exception("Repository | Have not this old asset");
                    }
                    AssetOld.State = (int)StateAsset.Avaiable;
                    Asset.State = (int)StateAsset.Assigned;
                    assignment.AssetId = assignmentRequestModel.AssetId;
                    assignment.AssetName = Asset.AssetName;
                }
                assignment.Note = assignmentRequestModel.Note;
                assignment.AssignedDate = assignmentRequestModel.AssignedDate;
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    var assignmentmodel = new AssignmentModel
                    {
                        AssignmentId = assignment.AssignmentId,
                        UserId = assignment.UserId,
                        AssignedTo = assignment.AssignTo,
                        AssetId = assignment.AssetId,
                        AssetName = assignment.AssetName,
                        AdminId = assignment.AdminId,
                        AssignedBy = assignment.AssignBy,
                        LocationId = assignment.LocationId,
                        AssignedDate = assignment.AssignedDate,
                        State = assignment.State
                    };
                    transaction.Commit();
                    return assignmentmodel;
                }
                else
                    throw new Exception("Repository | Update assignment fail");
            }
            catch
            {
                throw new Exception("Repository | Update assignment fail");
            }
        }
        public async Task<bool> ChangeStateAssignmentAsync(string id, StateAssignment state)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw new Exception("Repository | Have not this assignment");
            }
            assignment.State = (int)state;
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            throw new Exception("Repository | Change state assignment fail");
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw new Exception("Repository | Have not this assignment");
            }
            if (assignment.State.Equals((int)StateAssignment.Accepted) || assignment.State.Equals((int)StateAssignment.Completed))
            {
                throw new Exception("Repository | State is not valid");
            }
            var returnrequest = await _dbContext.ReturnRequests.FirstOrDefaultAsync(x => x.AssignmentId == assignment.AssignmentId);
            if (returnrequest != null)
            {
                throw new Exception("Repository | This assignment have a return request");
            }
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
            if (asset == null)
            {
                throw new Exception("Repository | Have not this asset");
            }
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                asset.State = (int)StateAsset.Avaiable;
                var deleted = _dbContext.Assignments.Remove(assignment);
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    transaction.Commit();
                    return true;
                }
                else
                    throw new Exception("Repository | Delete assignment fail");
            }
            catch
            {
                throw new Exception("Repository | Delete assignment fail");
            }
        }
        public async Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(MyAssignmentRequestParams myAssignmentRequestParams)
        {
            var queryable = _dbContext.Assignments.Where(x => x.LocationId == myAssignmentRequestParams.LocationId && x.UserId == myAssignmentRequestParams.UserId).AsQueryable();
            queryable = queryable.Include(x => x.Asset).ThenInclude(x => x.Category);
            queryable = queryable.Where(x => x.AssignedDate.Value.Date <= DateTime.Now.Date);
            queryable = queryable.Where(x => x.State == (int)StateAssignment.Accepted || x.State == (int)StateAssignment.WaitingForAcceptance);
            if (myAssignmentRequestParams.SortAssetId.HasValue)
            {
                if (myAssignmentRequestParams.SortAssetId.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetId);
            }
            else if (myAssignmentRequestParams.SortAssetName.HasValue)
            {
                if (myAssignmentRequestParams.SortAssetName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetName);
            }
            else if (myAssignmentRequestParams.SortCategoryName.HasValue)
            {
                if (myAssignmentRequestParams.SortCategoryName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Asset.Category.CategoryName);
                else
                    queryable = queryable.OrderByDescending(x => x.Asset.Category.CategoryName);
            }
            else if(myAssignmentRequestParams.SortAssignedDate.HasValue)
            {
                if (myAssignmentRequestParams.SortAssignedDate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedDate);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedDate);
            }
            else if(myAssignmentRequestParams.SortState.HasValue)
            {
                if (myAssignmentRequestParams.SortState.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.State);
                else
                    queryable = queryable.OrderByDescending(x => x.State);
            }
            else
            {
                queryable = queryable.OrderBy(x => x.AssetId);
            }
            var list = await queryable.Select(x => new MyAssigmentModel
            {
                AssignmentId = x.AssignmentId,
                UserId = x.UserId,
                AssignedTo = x.AssignTo,
                AssetId = x.AssetId,
                AdminId = x.AdminId,
                AssignedBy = x.AssignBy,
                LocationId = x.LocationId,
                AssetName = x.AssetName,
                AssignedDate = x.AssignedDate,
                State = x.State,
                CategoryName = x.Asset.Category.CategoryName
            }).ToListAsync();
            return list;
        }
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(AssignmentRequestParams assignmentRequestParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assignmentRequestParams.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            var queryable = _dbContext.Assignments.Include(x => x.Location).Where(x => x.LocationId == assignmentRequestParams.LocationId);
            queryable = queryable.Where(x => x.State != (int)StateAssignment.Completed);
            if (assignmentRequestParams.StateAssignments != null)
            {
                var StateNum = Array.ConvertAll(assignmentRequestParams.StateAssignments, value => (int)value);
                queryable = queryable.Where(x => StateNum.Contains(x.State));
            }
            if (!string.IsNullOrEmpty(assignmentRequestParams.AssignedDate))
            {
                var DateNum = Convert.ToDateTime(assignmentRequestParams.AssignedDate);
                queryable = queryable.Where(x => DateNum.Day == x.AssignedDate.Value.Day && DateNum.Month == x.AssignedDate.Value.Month && DateNum.Year == x.AssignedDate.Value.Year);
            }
            if (!string.IsNullOrEmpty(assignmentRequestParams.Query))
            {
                queryable = queryable.Where(x => x.AssetId.Contains(assignmentRequestParams.Query) || x.AssetName.Contains(assignmentRequestParams.Query)|| x.AssignTo.Contains(assignmentRequestParams.Query));
            }
            if (assignmentRequestParams.SortAssetId.HasValue)
            {
                if (assignmentRequestParams.SortAssetId.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetId);
            }
            else if (assignmentRequestParams.SortAssetName.HasValue)
            {
                if (assignmentRequestParams.SortAssetName.Value == SortBy.ASC)
                  queryable = queryable.OrderBy(x => x.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetName);
            }
            else if (assignmentRequestParams.SortAssignedBy.HasValue)
            {
                if (assignmentRequestParams.SortAssignedBy.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignBy);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignBy);
            }
            else if (assignmentRequestParams.SortAssignedTo.HasValue)
            {
                if (assignmentRequestParams.SortAssignedTo.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignTo);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignTo);
            }
            else if (assignmentRequestParams.SortAssignedDate.HasValue)
            {
                if (assignmentRequestParams.SortAssignedDate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedDate);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedDate);
            }
            else if (assignmentRequestParams.SortState.HasValue)
            {
                if (assignmentRequestParams.SortState.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.State);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignTo);
            }
            var totalitem = queryable.Count();

            var result = Paging<Assignment>(queryable, assignmentRequestParams.PageSize, assignmentRequestParams.Page);
            var list = await result.Sources.Select(x => new AssignmentModel
            {
                AssignmentId = x.AssignmentId,
                UserId= x.UserId,
                AdminId =x.AdminId,
                LocationId=x.LocationId,
               AssetId = x.AssetId,
               AssetName = x.AssetName,
               AssignedTo = x.AssignTo,
               AssignedBy = x.AssignBy,
               AssignedDate = x.AssignedDate,
               State = x.State,
            }).ToListAsync();
            return (list, result.TotalPage, totalitem);
        }
        public async Task<AssignmentDetailModel> GetAssignmentById(string id)
        {
            var assignment = await _dbContext.Assignments.Include(x => x.Asset).Include(x => x.Location).Include(x => x.User).FirstOrDefaultAsync(x => x.AssignmentId == id);
            if(assignment == null)
            {
                return null;
            }
            var assignmentModel = new AssignmentDetailModel
            {
                AssetId = assignment.AssetId,
                AssetName = assignment.AssetName,
                Specification = assignment.Asset.Specification,
                AssignedTo = assignment.AssignTo,
                AssignedBy = assignment.AssignBy,
                AssignedDate = assignment.AssignedDate.Value,
                State = assignment.State,
                Note = assignment.Note,
                LocationId = assignment.Location.LocationName,
                UserId = assignment.UserId,
                FullNameUser = assignment.User.FirstName + " " + assignment.User.LastName,
                AdminId = assignment.AdminId,
                AssignmentId = assignment.AssignmentId,
            };
            return assignmentModel;
        }
    }
}
