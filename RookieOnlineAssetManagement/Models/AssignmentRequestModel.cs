using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentRequestModel
    {
        public string AssignmentId { get; set; }
        public string UserId { get; set; }
        public string AssetId { get; set; }
        public string AdminId { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string Note { get; set; }
        public string LocationId { get; set; }
    }
}
