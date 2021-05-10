using System.Collections.Generic;
using System.Threading.Tasks;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;

namespace RookieOnlineAssetManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public Task<ICollection<UserModel>> GetListAsync()
        {
            return _userRepo.GetListAsync();
        }
    }
}