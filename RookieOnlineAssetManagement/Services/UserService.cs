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
            return _userRepo.CreateUserAsync(userRequest);
        }
    }
}