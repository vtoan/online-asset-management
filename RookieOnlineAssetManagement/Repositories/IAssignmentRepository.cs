using RookieOnlineAssetManagement.Models;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IAssignmentRepository
    {
        public Task<AssignmentModel> CreateAssignmentAsync();
    }
}