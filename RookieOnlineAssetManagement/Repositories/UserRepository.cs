using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace RookieOnlineAssetManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
                        queryable = queryable.OrderBy(item => item.FirstName).OrderBy(item => item.LastName);
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
                FullName = x.FirstName + " " + x.LastName,
                UserName = x.UserName,
                JoinedDate = x.JoinedDate,
                RoleName = x.Roles.First().NormalizedName,

            }).ToListAsync();
            var totalpage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return (list, totalpage);

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

                var password = username + "@" + userRequest.DateOfBirth.Value.ToString("ddMMyyyy");
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