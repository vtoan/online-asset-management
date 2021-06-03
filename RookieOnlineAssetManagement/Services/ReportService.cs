using RookieOnlineAssetManagement.Models;
using RookieOnlineAssetManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Services
{
    public class ReportService: IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<ICollection<ReportModel>> ExportReportAsync(ReportRequestParams reportRequestParams)
        {
            return await _reportRepository.ExportReportAsync(reportRequestParams);
        }
        public async Task<(ICollection<ReportModel> Datas, int TotalPage)> GetListReportAsync(ReportRequestParams reportParams)
        {
            return await _reportRepository.GetListReportAsync(reportParams);
        }
    }
}
