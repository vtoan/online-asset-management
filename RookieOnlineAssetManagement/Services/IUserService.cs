using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Services
{
    public interface IUserService
    {
        Task<ICollection<UserModel>> GetListAsync();
    }
}