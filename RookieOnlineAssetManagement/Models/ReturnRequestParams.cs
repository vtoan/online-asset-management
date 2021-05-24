using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReturnRequestParams
    {
        public bool[] StateReturnRequests { get; set; }
        public string ReturnedDate { get; set; }
        public string LocationId { get; set; }
        public string query { get; set; }
        public SortBy? sortAssetId { get; set; }
        public SortBy? sortAssetName { get; set; }
        public SortBy? sortRequestedBy { get; set; }
        public SortBy? sortAcceptedBy { get; set; }
        public SortBy? sortAssignedDate { get; set; }
        public SortBy? sortReturnedDate { get; set; }
        public SortBy? sortState { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
