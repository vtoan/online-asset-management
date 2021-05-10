using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReturnRequest
    {
        public long AssignmentId { get; set; }
        public string AcceptedUserId { get; set; }
        public string RequestUserId { get; set; }
        public string RequestBy { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool State { get; set; }
    }
}
