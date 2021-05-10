using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<UserModel> GetUserById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> UpdateUser(string id, UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> CreateUser(UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> DeleteUser(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}