using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize);

        Task<UserDetailModel> GetUserByIdAsync(string id);

        Task<UserRequestModel> CreateUserAsync(UserRequestModel userRequest);

        Task<UserRequestModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DisableUserAsync(string id);
    }
}