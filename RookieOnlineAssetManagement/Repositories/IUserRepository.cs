using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(UserRequestParmas userRequestParmas);

        Task<UserDetailModel> GetUserByIdAsync(string id);

        Task<UserModel> CreateUserAsync(UserRequestModel userRequest);

        Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DisableUserAsync(string id);
    }
}