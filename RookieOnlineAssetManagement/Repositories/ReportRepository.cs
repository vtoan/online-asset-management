using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public class ReportRepository: IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> ExportReportAsync(string locationId)
        {
            return true;
        }
        public async Task<ICollection<ReportModel>> GetListReportAsync(string locationId)
        {
            return null;
        }
    }
}
