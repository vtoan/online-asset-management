using System;
using System.Linq;
using System.Linq.Expressions;
using RookieOnlineAssetManagement.Enums;

namespace RookieOnlineAssetManagement.Utils
{
    public static class LinqExtension
    {

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, SortBy? orderMethod)
        {
            if (!orderMethod.HasValue) return q;
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = orderMethod.Value switch
            {
                SortBy.ASC => "OrderBy",
                SortBy.DESC => "OrderByDescending",
                _ => ""
            };
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}