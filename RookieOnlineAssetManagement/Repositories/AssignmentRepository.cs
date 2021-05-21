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
    public class AssignmentRepository: IAssignmentRepository
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
                return assignmentmodel;
            }
            return null;
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            if (!assignmentRequestModel.State.Equals((int)StateAssignment.WatingForAcceptance))
            { return null; }

            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
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
                return assignmentmodel;
            }
            return null;
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
            _dbContext.Assignments.Remove(assignment);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(string userid, string locationid, SortBy? AssetIdSort,SortBy? AssetNameSort, SortBy? CategoryNameSort, SortBy? AssignedDateSort, SortBy? StateSort)
        {
            var queryable = _dbContext.Assignments.Where(x => x.LocationId == locationid && x.UserId == userid).AsQueryable();
            queryable = queryable.Include(x => x.Asset).ThenInclude(x => x.Category);
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
        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(StateAssignment[] StateAssignments, string AssignedDateAssignment, string query, SortBy AssetId, SortBy AssetName, SortBy AssignedTo, SortBy AssignedBy, SortBy AssignedDate, SortBy State, int page, int pageSize)
        {
            return (null, 0, 0);
        }
        public async Task<AssetDetailModel> GetAssignmentById(string id)
        {
            return null;
        }
    }
}
