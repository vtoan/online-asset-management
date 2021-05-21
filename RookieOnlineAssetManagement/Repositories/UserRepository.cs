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

        public async Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(UserRequestParmas userRequestParmas)
        {
            var queryable = _dbContext.Users.Where(item => item.LocationId == userRequestParmas.locationId);
            if (!string.IsNullOrEmpty(userRequestParmas.query))
            {
                queryable = queryable.Where(x => x.Id.Contains(userRequestParmas.query) || x.UserName.Contains(userRequestParmas.query));
            }
            if (userRequestParmas.sortCode != null)
                switch (userRequestParmas.sortCode)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.StaffCode);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.StaffCode);
                        break;
                }
            if (userRequestParmas.sortFullName.HasValue)
            {
                switch (userRequestParmas.sortFullName)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.FirstName).ThenBy(x => x.LastName);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.FirstName).ThenBy(x => x.LastName);
                        break;
                }
            }
            else if (userRequestParmas.sortDate.HasValue)
            {
                switch (userRequestParmas.sortDate)
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

            if (userRequestParmas.type?.Length > 0)
            {
                var stringType = userRequestParmas.type.Select(x => x.ToString()).ToArray();
                queryable = queryable.Where(x => x.Roles.Any(r => stringType.Contains(r.NormalizedName)));
            }
            if (userRequestParmas.sortType.HasValue)
            {
                switch (userRequestParmas.sortType)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.Roles.First());
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.Roles.First());
                        break;
                }
            }

            var result = Paging<User>(queryable, userRequestParmas.pageSize, userRequestParmas.page);
            var list = await result.Sources.Select(x => new UserModel
            {
                UserId = x.Id,
                StaffCode = x.StaffCode,
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
                UserId = user.Id,
                StaffCode = user.StaffCode,
                FullName = user.FirstName + " " + user.LastName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                DateOfBirth = user.DateOfBirth.Value,
                Gender = user.Gender.Value,
                JoinedDate = user.JoinedDate,
                RoleName = role.NormalizedName,
                LocationName = user.Location.LocationName,
                LocationId = user.LocationId
            };
            return userdetail;
        }

        public async Task<UserModel> CreateUserAsync(UserRequestModel userRequest)
        {
            string username = userRequest.FirstName.ToLower();
            var firstChars = userRequest.LastName.Split(' ').Select(s => s[0]).ToArray();
            string prefix = new string(firstChars).ToLower();
            username = username + prefix;

            var UserExtension = await _dbContext.UserExtensions.FirstOrDefaultAsync(x => x.UserName == username);
            int number = _dbContext.UserExtensions.Sum(x => x.NumIncrease) + 1;

            var usermodel = new UserModel();

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
                usermodel.UserId = user.Id;
                usermodel.StaffCode = user.StaffCode;
                usermodel.FullName = user.FirstName + " " + user.LastName;
                usermodel.UserName = user.UserName;
                usermodel.JoinedDate = user.JoinedDate;
                usermodel.RoleName = role;
                usermodel.LocationId = user.LocationId;

                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                return null;
            }

            return usermodel;
        }

        public async Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest)
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
            // user.Id = userRequest.UserId;
            var role = userRequest.Type.ToString();
            user.DateOfBirth = userRequest.DateOfBirth.Value;
            user.Gender = userRequest.Gender;
            user.JoinedDate = userRequest.JoinedDate.Value;
            await _changeRoleUserAsync(user.Id, userRequest.Type);
            await _dbContext.SaveChangesAsync();

            var usermodel = new UserModel
            {
                UserId = user.Id,
                StaffCode = user.StaffCode,
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                JoinedDate = user.JoinedDate,
                RoleName = role,
                LocationId = user.LocationId
            };

            return usermodel;
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