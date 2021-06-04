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
    public class ReportRepository :BaseRepository,IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<ReportModel>> ExportReportAsync(ReportRequestParams reportRequestParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == reportRequestParams.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this location");
            }

            var queryable = _dbContext.Assets
              .Where(x => x.LocationId == reportRequestParams.LocationId)
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
              }).AsQueryable();
            queryable = this.SortData<ReportModel, ReportRequestParams>(queryable, reportRequestParams);
            return await queryable.ToListAsync();
        }
        public async Task<(ICollection<ReportModel> Datas, int TotalPage)> GetListReportAsync(ReportRequestParams reportParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == reportParams.LocationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this loaction");
            }

            var queryable = _dbContext.Assets
              .Where(x => x.LocationId == reportParams.LocationId)
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
              }).AsQueryable();
            queryable = this.SortData<ReportModel, ReportRequestParams>(queryable, reportParams);
            var result = Paging<ReportModel>(queryable, reportParams.PageSize, reportParams.Page);
            var list = await result.Sources.ToListAsync();
            return (list, result.TotalPage);
        }
    }
}
