using System;
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