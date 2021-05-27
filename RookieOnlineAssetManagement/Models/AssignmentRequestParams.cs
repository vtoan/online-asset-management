using Microsoft.AspNetCore.Mvc.ModelBinding;
using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentRequestParams
    {

        [BindNever]
        public string LocationId { get; set; }
        public StateAssignment[] StateAssignments { get; set; }
        public string AssignedDate { get; set; }
        public string Query { get; set; }
        public SortBy? SortAssetId { get; set; }
        public SortBy? SortAssetName { get; set; }
        public SortBy? SortAssignedTo { get; set; }
        public SortBy? SortAssignedBy { get; set; }
        public SortBy? SortAssignedDate { get; set; }
        public SortBy? SortState { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
