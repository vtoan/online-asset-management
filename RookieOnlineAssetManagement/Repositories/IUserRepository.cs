using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<UserModel>> GetListUserAsync(string locationId, TypeUser type, string query, SortBy? sortCode, SortBy? sortName, SortBy? sortDate, SortBy? sortType, int page, int pageSize);

        Task<UserModel> GetUserByIdAsync(string id);

        Task<UserModel> CreateUserAsync(UserRequestModel userRequest);

        Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DeleteUserAsync(string id);
    }
}