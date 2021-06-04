using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class MyAssignmentRequestParams
    {
        [ColumnSort("AssetId")]
        public SortBy? SortAssetId { get; set; }
        [ColumnSort("AssetId")]
        public SortBy? SortAssetName { get; set; }
        [ColumnSort("CategoryName")]
        public SortBy? SortCategoryName { get; set; }
        [ColumnSort("AssignedDate")]
        public SortBy? SortAssignedDate { get; set; }
        [ColumnSort("State")]
        public SortBy? SortState { get; set; }
        [BindNever]
        public string UserId { get; set; }
        [BindNever]
        public string LocationId { get; set; }

    }
}
