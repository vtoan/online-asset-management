using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class MyAssignmentRequestParams
    {
        public string UserId { get; set; }
        public string LocationId { get; set; }
        public SortBy? SortAssetId { get; set; }
        public SortBy? SortAssetName { get; set; }
        public SortBy? SortCategoryName { get; set; }
        public SortBy? SortAssignedDate { get; set; }
        public SortBy? SortState { get; set; }
    }
}
