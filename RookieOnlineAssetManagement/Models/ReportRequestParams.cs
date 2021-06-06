using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReportRequestParams
    {
        [ColumnSort("CategoryName")]
        public SortBy? SortCategoryName { get; set; }

        [ColumnSort("Total")]
        public SortBy? SortTotal { get; set; }

        [ColumnSort("AssignedTotal")]
        public SortBy? SortAssignedTotal { get; set; }

        [ColumnSort("AvailableTotal")]
        public SortBy? SortAvailableTotal { get; set; }
        
        [ColumnSort("NotAvailableTotal")]
        public SortBy? SortNotAvailableTotal { get; set; }
        
        [ColumnSort("WatingRecyclingTotal")]
        public SortBy? SortWatingRecyclingTotal { get; set; }
        
        [ColumnSort("RecycledTotal")]
        public SortBy? SortRecycledTotal { get; set; }

        [BindNever]
        public string LocationId { get; set; }

        public int Page { get; set; }
        
        public int PageSize { get; set; }
    }
}
