using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Exceptions;
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
        private RepoException e = new RepoException();
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<ReportModel>> ExportReportAsync(ReportRequestParams reportRequestParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId ==reportRequestParams.LocationId);
            if (location == null)
            {
                throw new RepoException("Have not this location");
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

            if (reportRequestParams.SortCategoryName.HasValue)
            {
                if (reportRequestParams.SortCategoryName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.CategoryName);
                else
                    queryable = queryable.OrderByDescending(x => x.CategoryName);
            }
            else if (reportRequestParams.SortTotal.HasValue)
            {
                if (reportRequestParams.SortTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Total);
                else
                    queryable = queryable.OrderByDescending(x => x.Total);
            }
            else if (reportRequestParams.SortAssignedTotal.HasValue)
            {
                if (reportRequestParams.SortAssignedTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedTotal);
            }
            else if (reportRequestParams.SortAvailableTotal.HasValue)
            {
                if (reportRequestParams.SortAvailableTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AvailableTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.AvailableTotal);
            }
            else if (reportRequestParams.SortNotAvailableTotal.HasValue)
            {
                if (reportRequestParams.SortNotAvailableTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.NotAvailableTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.NotAvailableTotal);
            }
            else if (reportRequestParams.SortRecycledTotal.HasValue)
            {
                if (reportRequestParams.SortRecycledTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.RecycledTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.RecycledTotal);
            }
            else if (reportRequestParams.SortWatingRecyclingTotal.HasValue)
            {
                if (reportRequestParams.SortWatingRecyclingTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.WatingRecyclingTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.WatingRecyclingTotal);
            }
            return await queryable.ToListAsync();
        }
        public async Task<(ICollection<ReportModel> Datas, int TotalPage)> GetListReportAsync(ReportRequestParams reportParams)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == reportParams.LocationId);
            if (location == null)
            {
                throw new RepoException("Have not this location");
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

            if (reportParams.SortCategoryName.HasValue)
            {
                if (reportParams.SortCategoryName.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.CategoryName);
                else
                    queryable = queryable.OrderByDescending(x => x.CategoryName);
            }
            else if (reportParams.SortTotal.HasValue)
            {
                if (reportParams.SortTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.Total);
                else
                    queryable = queryable.OrderByDescending(x => x.Total);
            }
            else if (reportParams.SortAssignedTotal.HasValue)
            {
                if (reportParams.SortAssignedTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AssignedTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.AssignedTotal);
            }
            else if (reportParams.SortAvailableTotal.HasValue)
            {
                if (reportParams.SortAvailableTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.AvailableTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.AvailableTotal);
            }
            else if (reportParams.SortNotAvailableTotal.HasValue)
            {
                if (reportParams.SortNotAvailableTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.NotAvailableTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.NotAvailableTotal);
            }
            else if (reportParams.SortRecycledTotal.HasValue)
            {
                if (reportParams.SortRecycledTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.RecycledTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.RecycledTotal);
            }
            else if (reportParams.SortWatingRecyclingTotal.HasValue)
            {
                if (reportParams.SortWatingRecyclingTotal.Value == SortBy.ASC)
                    queryable = queryable.OrderBy(x => x.WatingRecyclingTotal);
                else
                    queryable = queryable.OrderByDescending(x => x.WatingRecyclingTotal);
            }
            var result = Paging<ReportModel>(queryable, reportParams.PageSize, reportParams.Page);
            var list = await result.Sources.ToListAsync();
            return (list, result.TotalPage);
        }

    }
}
