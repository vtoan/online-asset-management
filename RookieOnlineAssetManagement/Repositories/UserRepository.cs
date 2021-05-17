using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Entities;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Utils;

namespace RookieOnlineAssetManagement.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize)
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
                        queryable = queryable.OrderBy(item => item.FirstName).OrderBy(item => item.LastName);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.FirstName).OrderBy(item => item.LastName);
                        break;
                }
            }
            else if (sortDate.HasValue)
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
            //include role
            queryable.Include(item => item.Roles);

            if (type.Length > 0)
            {
                var stringType = type.Select(x => x.ToString()).ToArray();
                queryable = queryable.Where(x => x.Roles.Any(r => stringType.Contains(r.NormalizedName)));
            }
            if (sortType.HasValue)
            {
                switch (sortType)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.Roles.First());
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.Roles.First());
                        break;
                }
            }

            var result = Paging<User>(queryable, pageSize, page);
            var list = await result.Sources.Select(x => new UserModel
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName,
                UserName = x.UserName,
                JoinedDate = x.JoinedDate,
                RoleName = x.Roles.First().NormalizedName,

            }).ToListAsync();
            return (list, result.TotalPage);
        }

        public async Task<UserDetailModel> GetUserByIdAsync(string id)
        {
            var user = await _dbContext.Users.Where(item => item.Id == id).Include(item => item.Location).FirstOrDefaultAsync();
            if (user == null) return null;
            var userRole = await _dbContext.UserRoles.Where(item => item.UserId == user.Id).FirstOrDefaultAsync();
            if (userRole == null) return null;
            var role = await _dbContext.Roles.Where(item => item.Id == userRole.RoleId).FirstOrDefaultAsync();
            if (role == null) return null;
            var userdetail = new UserDetailModel
            {
                Id = user.StaffCode,
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth.Value,
                Gender = user.Gender.Value,
                JoinedDate = user.JoinedDate,
                RoleName = role.NormalizedName,
                Location = user.Location.LocationName
            };
            return userdetail;
        }

        public async Task<UserRequestModel> CreateUserAsync(UserRequestModel userRequest)
        {
            string username = userRequest.FirstName.ToLower();
            var firstChars = userRequest.LastName.Split(' ').Select(s => s[0]).ToArray();
            string prefix = new string(firstChars).ToLower();
            username = username + prefix;

            var UserExtension = await _dbContext.UserExtensions.FirstOrDefaultAsync(x => x.UserName == username);
            int number = _dbContext.UserExtensions.Sum(x => x.NumIncrease) + 1;
            var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (UserExtension == null)
                {
                    short numincrease = 1;
                    var userextension = new UserExtension
                    {
                        UserName = username,
                        NumIncrease = numincrease
                    };
                    _dbContext.UserExtensions.Add(userextension);
                }
                else
                {
                    username = username + UserExtension.NumIncrease.ToString();
                    UserExtension.NumIncrease = (short)(UserExtension.NumIncrease + 1);
                }

                // var password = username + "@" + userRequest.DateOfBirth.Value.ToString("ddMMyyyy");
                var password = AccountHelper.GenerateAccountPass(username, userRequest.DateOfBirth.Value);
                var role = userRequest.Type.ToString();
                var staffcode = "SD" + number.ToString("0000");

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    UserName = username,
                    DateOfBirth = userRequest.DateOfBirth,
                    Gender = userRequest.Gender,
                    JoinedDate = userRequest.JoinedDate,
                    LocationId = userRequest.LocationId,
                    StaffCode = staffcode,
                    IsChange = false
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    return null;
                }

                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                return null;
            }
            return userRequest;
        }

        public async Task<UserRequestModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            if (user.FirstName != userRequest.FirstName || user.LastName != userRequest.LastName)
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
            var assignment = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.UserId == id);
            if (assignment != null)
            {
                return false;
            }
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
            // user.LockoutEnabled = true;
            // user.LockoutEnd = DateTime.MaxValue;
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