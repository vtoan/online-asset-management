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
        public string AssignedDateAssignment { get; set; }
        public string LocationId { get; set; }
        public string query { get; set; }
        public SortBy? AssetId { get; set; }
        public SortBy? AssetName { get; set; }
        public SortBy? AssignedTo { get; set; }
        public SortBy? AssignedBy { get; set; }
        public SortBy? AssignedDate { get; set; }
        public SortBy? State { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
