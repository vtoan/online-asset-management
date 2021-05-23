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
        
        public StateAssignment[] StateAssignments { get; set; }
        public string AssignedDate { get; set; }
        public string LocationId { get; set; }
        public string query { get; set; }
        public SortBy? sortAssetId { get; set; }
        public SortBy? sortAssetName { get; set; }
        public SortBy? sortAssignedTo { get; set; }
        public SortBy? sortAssignedBy { get; set; }
        public SortBy? sortAssignedDate { get; set; }
        public SortBy? sortState { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
