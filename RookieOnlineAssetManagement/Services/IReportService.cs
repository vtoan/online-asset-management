using RookieOnlineAssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public interface IReportService
    {
        Task<ICollection<ReportModel>> ExportReportAsync(string locationId);
        Task<(ICollection<ReportModel> Datas, int TotalPage)> GetListReportAsync(ReportRequestParams reportParams);
    }
}