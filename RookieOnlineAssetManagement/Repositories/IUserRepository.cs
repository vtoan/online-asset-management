using System.Collections.Generic;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IUserRepository
    {
        ICollection<UserModel> GetLists();
    }
}