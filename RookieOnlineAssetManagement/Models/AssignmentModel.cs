using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentModel
    {
        public long AssignmentId { get; set; }
        public string UserId { get; set; }
        public string AssetId { get; set; }
        public string AdminId { get; set; }
        public string LocationId { get; set; }
        public string AssignTo { get; set; }
        public string AssignBy { get; set; }
        public string AssetName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string CategoryName { get; set; }
        public string ShortName { get; set; }
        public bool State { get; set; }
    }
}
