using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

        public async Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page , int pageSize)
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

            queryable.Include(item => item.Roles);

            if (type.Length > 0)
            {
                var stringType = type.Select(x => x.ToString()).ToArray();
                queryable = queryable.Where(x => x.Roles.Any(r => stringType.Contains(r.NormalizedName)));
            }
            if (sortType.HasValue)
            {
                switch(sortType)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.Roles.First());
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.Roles.First());
                        break;
                }           
            }
            var totalRecord = queryable.Count();
            if(page > 0 && pageSize > 0)
            {
                queryable = queryable.Skip((page - 1) * pageSize).Take(pageSize);
            }
            var list = await queryable.Select(x => new UserModel
            {
                Id = x.Id,
                FullName = x.FirstName + "" + x.LastName,
                UserName = x.UserName,
                JoinedDate = x.JoinedDate,
                RoleName = x.Roles.ToString(),

            }).ToListAsync();
            var totalpage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return (list, totalpage);

        }

        public Task<UserModel> GetUserByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> CreateUserAsync(UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserRequestModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if(user == null)
            {
                return null;
            }
            user.DateOfBirth = userRequest.DateOfBirth.Value;
            user.Gender = userRequest.Gender;
            user.JoinedDate = userRequest.JoinedDate.Value;
            await _changeRoleUserAsync(user.Id, userRequest.Type);          
            await _dbContext.SaveChangesAsync();


            return userRequest;
        }
        
        public async Task<bool> DisableUserAsync(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTime.MaxValue;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task _changeRoleUserAsync(string userId, TypeUser typeUser)
        {
            if (typeUser == 0) return;
            var role = await _dbContext.Roles.Where(item => item.NormalizedName == typeUser.ToString()).FirstOrDefaultAsync();
            if (role == null) return;
            var roleUser = await _dbContext.UserRoles.Where(item => item.UserId == userId).FirstOrDefaultAsync();
            if (roleUser != null)
            _dbContext.UserRoles.Remove(roleUser);
            _dbContext.UserRoles.Add(new IdentityUserRole<string>() { UserId = userId, RoleId = role.Id });
        }

       
    }
}