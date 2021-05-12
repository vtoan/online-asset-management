using System;

namespace RookieOnlineAssetManagement.Entities
{
    public class ReturnRequest
    {
        public string AssignmentId { get; set; }
        public string AcceptedUserId { get; set; }
        public string RequestUserId { get; set; }
        public string RequestBy { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool State { get; set; }

        public User AcceptedUser { get; set; }
        public Assignment Assignment { get; set; }
        public User RequestUser { get; set; }
    }
}
