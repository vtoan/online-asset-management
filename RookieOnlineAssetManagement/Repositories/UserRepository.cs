using System.Collections.Generic;
using System.Linq;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;

namespace RookieOnlineAssetManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UserModel> GetLists()
        {
            return _dbContext.Users
                .Select(item => new UserModel { Id = item.Id, UserName = item.UserName, Email = item.Email })
                .ToList();
        }
    }
}