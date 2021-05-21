using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentModel
    {
        public string AssignmentId { get; set; }
        public string UserId { get; set; }
        public string AssignedTo { get; set; }
        public string AssetId { get; set; }
        public string AdminId { get; set; }
        public string AssignedBy { get; set; }
        public string LocationId { get; set; }
        public string AssetName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public int State { get; set; }
    }
}
