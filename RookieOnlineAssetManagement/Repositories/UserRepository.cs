using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ICollection<UserModel>> GetListAsync()
        {
            return await _dbContext.Users
                .Select(item => new UserModel { Id = item.Id, UserName = item.UserName, Email = item.Email })
                .ToListAsync();
        }

        public Task<UserModel> GetUserByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> CreateUserAsync(UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<UserModel> UpdateUserAsync(string id, UserRequestModel userRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}