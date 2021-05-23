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
        public string ReturnedDateFilter { get; set; }
        public string LocationId { get; set; }
        public string query { get; set; }
        public SortBy? AssetId { get; set; }
        public SortBy? AssetName { get; set; }
        public SortBy? RequestedBy { get; set; }
        public SortBy? AcceptedBy { get; set; }
        public SortBy? AssignedDate { get; set; }
        public SortBy? ReturnedDate { get; set; }
        public SortBy? State { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
