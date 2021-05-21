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
            var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
            var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
            var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
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
                State = (int)StateAssignment.WatingForAcceptance,
            };
            var transaction = _dbContext.Database.BeginTransaction();
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
                    return null;
                }
            }
            catch
            {
                return null;
            }
            
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            if (!assignmentRequestModel.State.Equals((int)StateAssignment.WatingForAcceptance))
            { return null; }

            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);

            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (assignment.UserId != assignmentRequestModel.UserId)
                {
                    var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
                    assignment.UserId = assignmentRequestModel.UserId;
                    assignment.AssignTo = AssignToUserId.UserName;
                }
                if (assignment.AdminId != assignmentRequestModel.AdminId)
                {
                    var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
                    assignment.AdminId = assignmentRequestModel.AdminId;
                    assignment.AssignBy = AssignByAdminId.UserName;
                }
                if (assignment.AssetId != assignmentRequestModel.AssetId)
                {
                    var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
                    var AssetOld = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
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
                    return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> ChangeStateAssignmentAsync(string id, StateAssignment state)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            assignment.State = (int)state;
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment.State.Equals((int)StateAssignment.Accepted))
            { return false; }
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                asset.State = (int)StateAsset.Avaiable;
                _dbContext.Assignments.Remove(assignment);
                var result = await _dbContext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(string userid, string locationid, SortBy? AssetIdSort,SortBy? AssetNameSort, SortBy? CategoryNameSort, SortBy? AssignedDateSort, SortBy? StateSort)
        {
            var queryable = _dbContext.Assignments.Where(x => x.LocationId == locationid && x.UserId == userid).AsQueryable();
            queryable = queryable.Include(x => x.Asset).ThenInclude(x => x.Category);
            queryable = queryable.Where(x => x.AssignedDate.Value.Date <= DateTime.Now.Date);
            if (AssetIdSort.HasValue)
            {
                if (AssetIdSort.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetId);
            }
            else if (AssetNameSort.HasValue)
            {
                if (AssetNameSort.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetName);
            }
            else if (CategoryNameSort.HasValue)
            {
                if (CategoryNameSort.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Asset.Category.CategoryName);
                else
                    queryable = queryable.OrderByDescending(x => x.Asset.Category.CategoryName);
            }
            else if(AssignedDateSort.HasValue)
            {
                if (AssignedDateSort.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedDate);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedDate);
            }
            else if(StateSort.HasValue)
            {
                if (StateSort.Value == SortBy.ASC)
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
            var queryable = _dbContext.Assignments.Include(x => x.Location).Where(x => x.LocationId == assignmentRequestParams.LocationId);
            if (assignmentRequestParams.StateAssignments != null)
            {
                var StateNum = Array.ConvertAll(assignmentRequestParams.StateAssignments, value => (int)value);
                queryable = queryable.Where(x => StateNum.Contains(x.State));
            }
            if (!string.IsNullOrEmpty(assignmentRequestParams.AssignedDateAssignment))
            {
                var DateNum = Convert.ToDateTime(assignmentRequestParams.AssignedDateAssignment);
                queryable = queryable.Where(x => DateNum.Day == x.AssignedDate.Value.Day && DateNum.Month == x.AssignedDate.Value.Month && DateNum.Year == x.AssignedDate.Value.Year);
            }
            if (!string.IsNullOrEmpty(assignmentRequestParams.query))
            {
                queryable = queryable.Where(x => x.AssetId.Contains(assignmentRequestParams.query) || x.AssetName.Contains(assignmentRequestParams.query)|| x.AssignTo.Contains(assignmentRequestParams.query));
            }
            if (assignmentRequestParams.AssetId.HasValue)
            {
                if (assignmentRequestParams.AssetId.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssetId);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetId);
            }
            else if (assignmentRequestParams.AssetName.HasValue)
            {
                if (assignmentRequestParams.AssetName.Value == SortBy.ASC)
                  queryable = queryable.OrderBy(x => x.AssetName);
                else
                    queryable = queryable.OrderByDescending(x => x.AssetName);
            }
            else if (assignmentRequestParams.AssignedBy.HasValue)
            {
                if (assignmentRequestParams.AssignedBy.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignBy);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignBy);
            }
            else if (assignmentRequestParams.AssignedTo.HasValue)
            {
                if (assignmentRequestParams.AssignedTo.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignTo);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignTo);
            }
            else if (assignmentRequestParams.AssignedDate.HasValue)
            {
                if (assignmentRequestParams.AssignedDate.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedDate);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedDate);
            }
            else if (assignmentRequestParams.State.HasValue)
            {
                if (assignmentRequestParams.State.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.State);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignTo);
            }
            var totalitem = queryable.Count();

            var result = Paging<Assignment>(queryable, assignmentRequestParams.pageSize, assignmentRequestParams.page);
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
            var assignment = await _dbContext.Assignments.Include(x=>x.Asset).Include(x =>x.Location).FirstOrDefaultAsync(x => x.AssignmentId == id);
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
                UserId= assignment.UserId,
                AdminId = assignment.AdminId,
                AssignmentId= assignment.AssignmentId,
            };
            return assignmentModel;
        }
    }
}
