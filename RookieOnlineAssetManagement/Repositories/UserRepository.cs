using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<UserModel>> GetListUserAsync(string locationId, TypeUser? type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize)
        {
            var queryable = _dbContext.Users.Where(item => item.LocationId == locationId);
            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.Id.Contains(query) || x.UserName.Contains(query));
            }
            if (sortCode != null)
                switch (sortCode)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.StaffCode);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.StaffCode);
                        break;
                }
            if (sortFullName.HasValue)
            {
                switch (sortFullName)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.FirstName).OrderBy(item=>item.LastName);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.FirstName).OrderBy(item => item.LastName);
                        break;
                }              
            }

            if (sortDate.HasValue)
            {
                switch (sortDate)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.JoinedDate);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.JoinedDate);
                        break;
                }
            }

            
            queryable
             .Join(
                      _dbContext.UserRoles,
                      user => user.Id,
                      userRole => userRole.UserId,
                      (user, userrole) => new
                      {
                          UserId = user.Id,
                          RoleId = userrole.RoleId,
                          StaffCode = user.StaffCode,
                          FullName = $"{user.FirstName} {user.LastName}",
                          UserName = user.UserName,
                          JoinedDate = user.JoinedDate
                      }
                  )
             .Join(
                       _dbContext.Roles,
                       userRole => userRole.RoleId,
                       role => role.Id,
                       (userRole, role) => new
                       {
                           RoleName = role.Name,

                       }
                  );

            //if (type.HasValue)
            //{
            //    queryable = queryable.Where(x => x.);
            //}
            
        }

        public Task<UserModel> GetUserByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> CreateUserAsync(UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}