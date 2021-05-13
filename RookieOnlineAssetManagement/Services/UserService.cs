using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;

namespace RookieOnlineAssetManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize)
        {
            return _userRepo.GetListUserAsync(locationId, type, query, sortCode, sortFullName, sortDate, sortType, page, pageSize);
        }
        public Task<UserRequestModel> CreateUserAsync(UserRequestModel userRequest)
        {
            var checkage = CheckDateAgeGreaterThan(18, userRequest.DateOfBirth.Value);
            var checkjoineddate = CheckIsSaturdayOrSunday(userRequest.JoinedDate.Value);
            var checkjoineddategreaterthanbirthofdate = CheckDateGreaterThan(userRequest.DateOfBirth.Value, userRequest.JoinedDate.Value);
            if (checkage == false)
            {
                return Task.FromResult<UserRequestModel>(null);
            }
            if (checkjoineddate == true)
            {
                return Task.FromResult<UserRequestModel>(null);
            }
            if (checkjoineddategreaterthanbirthofdate == false)
            {
                return Task.FromResult<UserRequestModel>(null);
            }
            return _userRepo.CreateUserAsync(userRequest);
        }
        public Task<UserDetailModel> GetUserByIdAsync(string id)
        {
            return _userRepo.GetUserByIdAsync(id);
        }
        public bool CheckDateGreaterThan(DateTime SmallDate, DateTime BigDate)
        {
            if (SmallDate > BigDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool CheckDateAgeGreaterThan(int age, DateTime BirthOfDate)
        {
            if(DateTime.Now.Year-BirthOfDate.Year>=18)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckIsSaturdayOrSunday(DateTime JoinedDate)
        {
            var dayofweek = JoinedDate.DayOfWeek;
            if (dayofweek == DayOfWeek.Sunday || dayofweek == DayOfWeek.Sunday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}