using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Repositories
{
    public interface IReportRepository
    {
        Task<bool> ExportReportAsync(string locationId);
        Task<ICollection<ReportModel>> GetListReportAsync(string locationId);
    }
}