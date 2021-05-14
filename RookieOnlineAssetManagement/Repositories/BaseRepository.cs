using System;
using System.Collections.Generic;
using System.Linq;

namespace RookieOnlineAssetManagement.Repositories
{
    public abstract class BaseRepository
    {
        protected (IQueryable<T> Sources, int TotalPage) Paging<T>(IQueryable<T> queryable, int pageSize, int page)
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
            return (queryable, totalPage);
        }
    }
}