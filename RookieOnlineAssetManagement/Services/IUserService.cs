using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Services
{
    public interface IUserService
    {
        Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(UserRequestParmas userRequestParmas);

        Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DisableUserAsync(string id);
        Task<UserModel> CreateUserAsync(UserRequestModel userRequest);
        Task<UserDetailModel> GetUserByIdAsync(string id);
        Task<string> GetDefaultPassword(string id);
    }
}