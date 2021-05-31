using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [BindNever]
        public string LocationId { get; set; }
        public string Query { get; set; }
        public SortBy? SortAssetId { get; set; }
        public SortBy? SortAssetName { get; set; }
        public SortBy? SortRequestedBy { get; set; }
        public SortBy? SortAcceptedBy { get; set; }
        public SortBy? SortAssignedDate { get; set; }
        public SortBy? SortReturnedDate { get; set; }
        public SortBy? SortState { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
