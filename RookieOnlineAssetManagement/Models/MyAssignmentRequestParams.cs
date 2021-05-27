using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class MyAssignmentRequestParams
    {
        [BindNever]
        public string UserId { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public SortBy? SortAssetId { get; set; }
        public SortBy? SortAssetName { get; set; }
        public SortBy? SortCategoryName { get; set; }
        public SortBy? SortAssignedDate { get; set; }
        public SortBy? SortState { get; set; }
    }
}
