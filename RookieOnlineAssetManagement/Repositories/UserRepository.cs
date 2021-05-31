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
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == userRequestParmas.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            var queryable = _dbContext.Users.Where(item => item.LocationId == userRequestParmas.LocationId);
            if (!string.IsNullOrEmpty(userRequestParmas.Query))
            {
                queryable = queryable.Where(x => x.StaffCode.Contains(userRequestParmas.Query) || x.UserName.Contains(userRequestParmas.Query) || x.FirstName.Contains(userRequestParmas.Query) || x.LastName.Contains(userRequestParmas.Query));
            }
            queryable = queryable.Where(x => x.LockoutEnabled == false);
            if (userRequestParmas.SortCode != null)
                switch (userRequestParmas.SortCode)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.StaffCode);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.StaffCode);
                        break;
                }
            if (userRequestParmas.SortFullName.HasValue)
            {
                switch (userRequestParmas.SortFullName)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.FirstName).ThenBy(x => x.LastName);
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.FirstName).ThenBy(x => x.LastName);
                        break;
                }
            }
            else if (userRequestParmas.SortDate.HasValue)
            {
                switch (userRequestParmas.SortDate)
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

            if (userRequestParmas.Type?.Length > 0)
            {
                var stringType = userRequestParmas.Type.Select(x => x.ToString()).ToArray();
                queryable = queryable.Where(x => x.Roles.Any(r => stringType.Contains(r.NormalizedName)));
            }
            if (userRequestParmas.SortType.HasValue)
            {
                switch (userRequestParmas.SortType)
                {
                    case SortBy.ASC:
                        queryable = queryable.OrderBy(item => item.Roles.First());
                        break;
                    case SortBy.DESC:
                        queryable = queryable.OrderByDescending(item => item.Roles.First());
                        break;
                }
            }

            var result = Paging<User>(queryable, userRequestParmas.PageSize, userRequestParmas.Page);
            var list = await result.Sources.Select(x => new UserModel
            {
                UserId = x.Id,
                StaffCode = x.StaffCode,
                FullName = x.FirstName + " " + x.LastName,
                UserName = x.UserName,
                JoinedDate = x.JoinedDate,
                RoleName = x.Roles.First().NormalizedName,
                Status = x.LockoutEnabled

            }).ToListAsync();
            return (list, result.TotalPage);
        }

        public async Task<UserDetailModel> GetUserByIdAsync(string id)
        {
            var user = await _dbContext.Users.Where(item => item.Id == id).Include(item => item.Location).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Repository | Have not this user");
            }
            var userRole = await _dbContext.UserRoles.Where(item => item.UserId == user.Id).FirstOrDefaultAsync();
            if (userRole == null)
            {
                throw new Exception("Repository | Have not user role");
            }
            var role = await _dbContext.Roles.Where(item => item.Id == userRole.RoleId).FirstOrDefaultAsync();
            if (role == null)
            {
                throw new Exception("Repository | Have not role");
            }
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
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == userRequest.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            string username = userRequest.FirstName.ToLower();
            var firstChars = userRequest.LastName.Split(' ').Select(s => s[0]).ToArray();
            string prefix = new string(firstChars).ToLower();
            username = username + prefix;

            var UserExtension = await _dbContext.UserExtensions.FirstOrDefaultAsync(x => x.UserName == username);
            int number = _dbContext.UserExtensions.Sum(x => x.NumIncrease) + 1;

            var usermodel = new UserModel();

            using var transaction = _dbContext.Database.BeginTransaction();
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
                return usermodel;
            }
            catch
            {
                throw new Exception("Repository | Create user fail");
            }
        }

        public async Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == userRequest.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Repository | Have not this user");
            }
            if (user.FirstName != userRequest.FirstName || user.LastName != userRequest.LastName)
            {
                throw new Exception("Repository | First name or Last name is not valid");
            }
            // user.Id = userRequest.UserId;
            var checkrole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == userRequest.Type.ToString().ToUpper());
            if (checkrole == null)
            {
                throw new Exception("Repository | Have not this role");
            }
            var role = userRequest.Type.ToString();
            user.DateOfBirth = userRequest.DateOfBirth.Value;
            user.Gender = userRequest.Gender;
            user.JoinedDate = userRequest.JoinedDate.Value;
            await _changeRoleUserAsync(user.Id, userRequest.Type);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
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
            throw new Exception("Repository | Update user fail");
        }

        public async Task<bool> DisableUserAsync(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Repository | Have not this user");
            }
            var assignments = await _dbContext.Assignments.Where(x => x.UserId == id).ToListAsync();
            if (assignments.Count > 0)
            {
                for (int i = 0; i < assignments.Count; i++)
                {
                    if (assignments[i].State == (int)StateAssignment.Accepted)
                    {
                        throw new Exception("Repository | Have not this assignment");
                    }
                }
                
            }
            var createResultLockend = await _userManager.SetLockoutEnabledAsync(user, true);
            if (!createResultLockend.Succeeded)
            {
                return false;
            }
            await _userManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
            // user.LockoutEnabled = true;
            // user.LockoutEnd = DateTime.MaxValue;
            var result = await _dbContext.SaveChangesAsync();

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