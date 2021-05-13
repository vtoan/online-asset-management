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

        public async Task<bool> DisableUserAsync(string id)
        {
            return await _userRepo.DisableUserAsync(id);
        }

        public async Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize)
        {
            return await _userRepo.GetListUserAsync(locationId, type, query, sortCode, sortFullName, sortDate, sortType, page, pageSize);
        }

        public async Task<UserRequestModel> UpdateUserAsync(string id,UserRequestModel userRequest)
        {
            return await _userRepo.UpdateUserAsync(id,userRequest);
        }
    }
}