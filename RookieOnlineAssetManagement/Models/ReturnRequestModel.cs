using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReturnRequestModel
    {
        public string AssignmentId { get; set; }
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public string AcceptedUserId { get; set; }
        public string RequestUserId { get; set; }
        public string RequestBy { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public bool State { get; set; }
    }
}
