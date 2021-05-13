using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        Task<(ICollection<UserModel> Datas, int TotalPage)> GetListUserAsync(string locationId, TypeUser[] type, string query, SortBy? sortCode, SortBy? sortFullName, SortBy? sortDate, SortBy? sortType, int page, int pageSize);

        Task<UserModel> GetUserByIdAsync(string id);

        Task<UserModel> CreateUserAsync(UserRequestModel userRequest);

        Task<UserRequestModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DisableUserAsync(string id);
    }
}