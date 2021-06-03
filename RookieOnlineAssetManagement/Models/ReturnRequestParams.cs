using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReturnRequestParams
    {
        [ColumnSort("AssetId")]
        public SortBy? SortAssetId { get; set; }
        [ColumnSort("AssetName")]
        public SortBy? SortAssetName { get; set; }
        [ColumnSort("RequestBy")]
        public SortBy? SortRequestedBy { get; set; }
        [ColumnSort("AcceptedBy")]
        public SortBy? SortAcceptedBy { get; set; }
        [ColumnSort("AssignedDate")]
        public SortBy? SortAssignedDate { get; set; }
        [ColumnSort("ReturnedDate")]
        public SortBy? SortReturnedDate { get; set; }
        [ColumnSort("State")]
        public SortBy? SortState { get; set; }
        public bool[] StateReturnRequests { get; set; }
        public string ReturnedDate { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public string Query { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
