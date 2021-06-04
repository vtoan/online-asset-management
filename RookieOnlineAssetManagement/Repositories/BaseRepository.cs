using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Data;
using RookieOnlineAssetManagement.Enums;
using RookieOnlineAssetManagement.Utils;

namespace RookieOnlineAssetManagement.Repositories
{
    public abstract class BaseRepository
    {

        protected async Task<bool> LocationIsExist(ApplicationDbContext dbContext, string locationId)
        {
            var location = await dbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == locationId);
            if (location == null)
            {
                throw new Exception("Repository | Have not this loaction");
            }
            return true;
        }

        protected (IQueryable<T> Sources, int TotalPage, int TotalItem) Paging<T>(IQueryable<T> queryable, int pageSize, int page)
        {
            if (pageSize > 0 && page > 0)
            {
                var totalItem = queryable.Count();
                var offset = page - 1;
                if (offset > 0)
                {
                    if (offset * pageSize > totalItem) throw new Exception("Offset is outbound");
                    queryable = queryable.Skip(offset * pageSize);
                }
                if (pageSize > 0) queryable = queryable.Take(pageSize);
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                return (queryable, totalPage, totalItem);
            }
            else
            {
                return (queryable, 0, 0);
            }
        }

        protected IQueryable<T> SortData<T, K>(IQueryable<T> q, K parmasModel) where K : notnull
        {
            var colSortFirst = _getColumnSortFirst<K>(parmasModel);
            if (!colSortFirst.HasValue) return q;
            return q.OrderByField(colSortFirst.Value.SortField, colSortFirst.Value.Method);
        }

        private Nullable<(string SortField, SortBy Method)> _getColumnSortFirst<K>(K parmasModel)
        {
            var props = parmasModel.GetType().GetProperties();
            foreach (var p in props)
            {
                ColumnSort cusAttr = (ColumnSort)Attribute.GetCustomAttribute(p, typeof(ColumnSort));
                if (cusAttr != null)
                {
                    var sortField = cusAttr.NameColumn;
                    var metho = p.GetValue(parmasModel);
                    if (metho != null)
                        return (sortField, (SortBy)metho);
                }
            }
            return null;
        }
    }
}