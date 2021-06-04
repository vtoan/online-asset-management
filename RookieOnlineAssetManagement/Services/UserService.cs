using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Exceptions;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using RookieOnlineAssetManagement.Utils;

namespace RookieOnlineAssetManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private ServiceException e;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            e = new ServiceException();
        }

        public async Task<bool> DisableUserAsync(string id)
        {
            return await _userRepo.DisableUserAsync(id);
        }

        public async Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(UserRequestParmas userRequestParmas)
        {
            return await _userRepo.GetListUserAsync(userRequestParmas);
        }

        public async Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            return await _userRepo.UpdateUserAsync(id, userRequest);
        }

        public async Task<string> GetDefaultPassword(string id)
        {
            var user = await GetUserByIdAsync(id);
            return AccountHelper.GenerateAccountPass(user.UserName, user.DateOfBirth);
        }

        public Task<UserModel> CreateUserAsync(UserRequestModel userRequest)
        {
            DayOfWeek dayofweek;
            var checkage = DateTimeHelper.CheckAgeGreaterThan(18, userRequest.DateOfBirth.Value);
            var checkjoineddate =DateTimeHelper.CheckIsSaturdayOrSunday(userRequest.JoinedDate.Value, out dayofweek);
            var checkjoineddategreaterthanbirthofdate = DateTimeHelper.CheckDateGreaterThan(userRequest.DateOfBirth.Value, userRequest.JoinedDate.Value);
            if (checkage == false)
            {
                throw e.AgeNoValidException();
            }
            if (checkjoineddate == true)
            {
                throw e.JoinDateException();
            }
            if (checkjoineddategreaterthanbirthofdate == false)
            {
                throw e.CompareDateException();
            }
            return _userRepo.CreateUserAsync(userRequest);
        }
        public Task<UserDetailModel> GetUserByIdAsync(string id)
        {
            return _userRepo.GetUserByIdAsync(id);
        }
    }
}