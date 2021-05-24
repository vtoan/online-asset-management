using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class MyAssignmentRequestParams
    {
        public string userId { get; set; }
        public string locationId { get; set; }
        public SortBy? sortAssetId { get; set; }
        public SortBy? sortAssetName { get; set; }
        public SortBy? sortCategoryName { get; set; }
        public SortBy? sortAssignedDate { get; set; }
        public SortBy? sortState { get; set; }
    }
}
