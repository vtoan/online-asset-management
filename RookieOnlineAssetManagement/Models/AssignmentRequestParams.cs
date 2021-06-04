using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Atributes;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentRequestParams
    {
        [ColumnSort("AssetId")]
        public SortBy? SortAssetId { get; set; }
        [ColumnSort("AssetId")]
        public SortBy? SortAssetName { get; set; }
        [ColumnSort("AssignTo")]
        public SortBy? SortAssignedTo { get; set; }
        [ColumnSort("AssignBy")]
        public SortBy? SortAssignedBy { get; set; }
        [ColumnSort("AssignedDate")]
        public SortBy? SortAssignedDate { get; set; }
        [ColumnSort("State")]
        public SortBy? SortState { get; set; }
        [BindNever]
        public string LocationId { get; set; }
        public StateAssignment[] StateAssignments { get; set; }
        public string AssignedDate { get; set; }
        public string Query { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
