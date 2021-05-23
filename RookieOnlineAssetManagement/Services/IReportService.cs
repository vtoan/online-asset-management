using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IReportService
    {
        Task<bool> ExportReportAsync(string locationId);
        Task<ICollection<ReportModel>> GetListReportAsync(string locationId)
    }
}