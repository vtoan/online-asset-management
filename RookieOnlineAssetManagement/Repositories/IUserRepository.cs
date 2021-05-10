using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<UserModel>> GetListAsync();

        Task<UserModel> GetUserByIdAsync(string id);

        Task<UserModel> CreateUserAsync(UserRequestModel userRequest);

        Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest);

        Task<bool> DeleteUserAsync(string id);
    }
}