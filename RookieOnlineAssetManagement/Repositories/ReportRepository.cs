using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<ICollection<ReportModel>> ExportReportAsync(string locationId)
        {
            var report = await _dbContext.Assets
                .Where(x => x.LocationId == locationId)
                .Include(x => x.Category)
                .GroupBy(x => x.Category.CategoryName, (c, t) => new ReportModel()
                {
                    CategoryName = c,
                    Total = t.Count(),
                    AssignedTotal = t.Where(x => x.State == (short)StateAsset.Assigned).Count(),
                    AvailableTotal = t.Where(x => x.State == (short)StateAsset.Avaiable).Count(),
                    NotAvailableTotal = t.Where(x => x.State == (short)StateAsset.NotAvaiable).Count(),
                    WatingRecyclingTotal = t.Where(x => x.State == (short)StateAsset.WatingRecycling).Count(),
                    RecycledTotal = t.Where(x => x.State == (short)StateAsset.Recycled).Count()
                }).ToListAsync();

            return report;
        }
        public async Task<ICollection<ReportModel>> GetListReportAsync(string locationId)
        {
            return null;
        }
    }
}
