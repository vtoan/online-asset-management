using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        ICollection<UserModel> GetLists();

        Task<UserModel> GetUserById(string id);

        Task<UserModel> CreateUser(UserRequestModel userRequest );

        Task<UserModel> UpdateUser(string id, UserRequestModel userRequest);

        Task<UserModel> DeleteUser(string id);
    }
}