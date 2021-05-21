using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Entities;
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
            return null;
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            return null;
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            return true;
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
