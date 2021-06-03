using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReportRequestParams
    {
        [BindNever]
        public string LocationId { get; set; }
        public SortBy? SortCategoryName { get; set; }
        public SortBy? SortTotal { get; set; }
        public SortBy? SortAssignedTotal { get; set; }
        public SortBy? SortAvailableTotal { get; set; }
        public SortBy? SortNotAvailableTotal { get; set; }
        public SortBy? SortWatingRecyclingTotal { get; set; }
        public SortBy? SortRecycledTotal { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
