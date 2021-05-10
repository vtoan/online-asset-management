using System.Collections.Generic;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Services
{
    public interface IUserService
    {
        ICollection<UserModel> GetLists();
    }
}