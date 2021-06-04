using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Exceptions;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class AssignmentRepository : BaseRepository, IAssignmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private RepoException e;
        public AssignmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            e = new RepoException();
        }
        public async Task<AssignmentModel> CreateAssignmentAsync(AssignmentRequestModel assignmentRequestModel)
        {
            await this.LocationIsExist(_dbContext, assignmentRequestModel.LocationId);
            var returnrequest = await _dbContext.ReturnRequests.FirstOrDefaultAsync(x => x.AssignmentId == assignmentRequestModel.AssignmentId);
            if (returnrequest != null)
            {
                throw e.RetuenRequestExistsException();
            }
            var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
            if (AssignToUserId == null)
            {
                throw e.AssignToUserException();
            }
            var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
            if (AssignByAdminId == null)
            {
                throw e.AssignByAdminException();
            }
            var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
            if (Asset == null)
            {
                throw e.AssetException();
            }
            if (Asset.State != (short)StateAsset.Avaiable)
            {
                throw e.NotAvailableException();
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
                    throw e.CreateAssignmentException();
                }
            }
            catch
            {
                throw e.CreateAssignmentException();
            }
        }
        public async Task<AssignmentModel> UpdateAssignmentAsync(string id, AssignmentRequestModel assignmentRequestModel)
        {
            await this.LocationIsExist(_dbContext, assignmentRequestModel.LocationId);
            if (!assignmentRequestModel.State.Equals((int)StateAssignment.WaitingForAcceptance))
            {
                throw e.SateAsignmentException();
            }

            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw e.AssignmentException();
            }
            if (assignment.UserId != assignmentRequestModel.UserId)
            {
                var AssignToUserId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.UserId);
                if (AssignToUserId == null)
                {
                    throw e.AssignToUserException();
                }
                assignment.UserId = assignmentRequestModel.UserId;
                assignment.AssignTo = AssignToUserId.UserName;
            }
            if (assignment.AdminId != assignmentRequestModel.AdminId)
            {
                var AssignByAdminId = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == assignmentRequestModel.AdminId);
                if (AssignByAdminId == null)
                {
                    throw e.AssignByAdminException();
                }
                assignment.AdminId = assignmentRequestModel.AdminId;
                assignment.AssignBy = AssignByAdminId.UserName;
            }
            if (assignment.AssetId != assignmentRequestModel.AssetId)
            {
                var Asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignmentRequestModel.AssetId);
                if (Asset == null)
                {
                    throw e.AssetException();
                }
                if (Asset.State != (short)StateAsset.Avaiable)
                {
                    throw e.NotAvailableException();
                }
                var AssetOld = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
                if (AssetOld == null)
                {
                    throw e.ErrorAssetException();
                }
                AssetOld.State = (int)StateAsset.Avaiable;
                Asset.State = (int)StateAsset.Assigned;
                assignment.AssetId = assignmentRequestModel.AssetId;
                assignment.AssetName = Asset.AssetName;
            }
            assignment.Note = assignmentRequestModel.Note;
            if (assignment.AssignedDate != assignmentRequestModel.AssignedDate)
            {
                if (DateTimeHelper.CheckDateGreaterThan(DateTime.Now, assignmentRequestModel.AssignedDate.Value) == false)
                {
                    throw e.CheckDateAssignmentException();
                }
                assignment.AssignedDate = assignmentRequestModel.AssignedDate;
            }

            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
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
                    throw e.UpdateAssignmentException();
            }
            catch
            {
                throw e.UpdateAssignmentException();
            }
        }
        public async Task<bool> ChangeStateAssignmentAsync(string id, StateAssignment state)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw e.AssignmentException();
            }
            assignment.State = (int)state;
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            throw e.ChangeStateException();
        }
        public async Task<bool> DeleteAssignmentAsync(string id)
        {
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
            {
                throw e.AssignmentException();
            }
            if (assignment.State.Equals((int)StateAssignment.Accepted) || assignment.State.Equals((int)StateAssignment.Completed))
            {
                throw e.NotValidException();
            }
            var returnrequest = await _dbContext.ReturnRequests.FirstOrDefaultAsync(x => x.AssignmentId == assignment.AssignmentId);
            if (returnrequest != null)
            {
                throw e.AssignmentSateExistsException();
            }
            var asset = await _dbContext.Assets.FirstOrDefaultAsync(x => x.AssetId == assignment.AssetId);
            if (asset == null)
            {
                throw e.AssetException();
            }
            if (asset.State == (short)StateAsset.Assigned)
            {
                throw e.AssetIsAssignException();
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
                    throw e.DeleteAssigmentException();
            }
            catch
            {
                throw e.DeleteAssigmentException();
            }
        }
        public async Task<ICollection<MyAssigmentModel>> GetListMyAssignmentAsync(MyAssignmentRequestParams myAssignmentRequestParams)
        {
            await this.LocationIsExist(_dbContext, myAssignmentRequestParams.LocationId);
            var queryable = _dbContext.Assignments.Where(x => x.LocationId == myAssignmentRequestParams.LocationId && x.UserId == myAssignmentRequestParams.UserId).AsQueryable();
            queryable = queryable.Include(x => x.Asset).ThenInclude(x => x.Category);
            queryable = queryable.Where(x => x.AssignedDate.Value.Date <= DateTime.Now.Date);
            queryable = queryable.Where(x => x.State == (int)StateAssignment.Accepted || x.State == (int)StateAssignment.WaitingForAcceptance);
            //sort
            var q = queryable.Select(x => new MyAssigmentModel
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
            });
            q = this.SortData<MyAssigmentModel, MyAssignmentRequestParams>(q, myAssignmentRequestParams);
            var list = await queryable.Include(x => x.ReturnRequest).ToListAsync();
            var AssignmentList = new List<MyAssigmentModel>();
            foreach (var x in list)
            {
                var AssignmentModel = new MyAssigmentModel
                {
                    AssignmentId = x.AssignmentId,
                    UserId = x.UserId,
                    AdminId = x.AdminId,
                    LocationId = x.LocationId,
                    AssetId = x.AssetId,
                    AssetName = x.AssetName,
                    AssignedTo = x.AssignTo,
                    AssignedBy = x.AssignBy,
                    AssignedDate = x.AssignedDate,
                    State = x.State,
                    CategoryName = x.Asset.Category.CategoryName
                };
                if (x.ReturnRequest != null)
                {
                    AssignmentModel.IsReturning = true;
                    AssignmentList.Add(AssignmentModel);
                }
                else
                {
                    AssignmentModel.IsReturning = false;
                    AssignmentList.Add(AssignmentModel);
                }
            }
            return AssignmentList;
        }

        public async Task<(ICollection<AssignmentModel> Datas, int TotalPage, int TotalItem)> GetListAssignmentAsync(AssignmentRequestParams assignmentRequestParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == assignmentRequestParams.LocationId);
            if (location == null)
            {
                throw e.LocationException();
            }
            var queryable = _dbContext.Assignments
                .Include(x => x.Location)
                .AsSplitQuery()
                .Where(x => x.LocationId == assignmentRequestParams.LocationId);
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
                queryable = queryable.Where(x => x.AssetId.Contains(assignmentRequestParams.Query) || x.AssetName.Contains(assignmentRequestParams.Query) || x.AssignTo.Contains(assignmentRequestParams.Query));
            }
            //sort
            queryable = this.SortData<Assignment, AssignmentRequestParams>(queryable, assignmentRequestParams);
            //paging
            var result = Paging<Assignment>(queryable, assignmentRequestParams.PageSize, assignmentRequestParams.Page);
            var list = await result.Sources.Include(x => x.ReturnRequest).ToListAsync();
            var AssignmentList = new List<AssignmentModel>();
            foreach (var x in list)
            {
                var AssignmentModel = new AssignmentModel
                {
                    AssignmentId = x.AssignmentId,
                    UserId = x.UserId,
                    AdminId = x.AdminId,
                    LocationId = x.LocationId,
                    AssetId = x.AssetId,
                    AssetName = x.AssetName,
                    AssignedTo = x.AssignTo,
                    AssignedBy = x.AssignBy,
                    AssignedDate = x.AssignedDate,
                    State = x.State,
                };
                if (x.ReturnRequest != null)
                {
                    AssignmentModel.IsReturning = true;
                    AssignmentList.Add(AssignmentModel);
                }
                else
                {
                    AssignmentModel.IsReturning = false;
                    AssignmentList.Add(AssignmentModel);
                }
            }
            return (AssignmentList, result.TotalPage, result.TotalItem);
        }
        public async Task<AssignmentDetailModel> GetAssignmentById(string id)
        {
            var assignment = await _dbContext.Assignments.Include(x => x.Asset).Include(x => x.Location).Include(x => x.User).FirstOrDefaultAsync(x => x.AssignmentId == id);
            if (assignment == null)
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
