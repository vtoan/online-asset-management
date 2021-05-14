using System;

namespace RookieOnlineAssetManagement.Entities
{
    public class Assignment
    {
        public string AssignmentId { get; set; }
        public string UserId { get; set; }
        public string AssetId { get; set; }
        public string AdminId { get; set; }
        public string AssignTo { get; set; }
        public string AssignBy { get; set; }
        public string AssetName { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string Note { get; set; }
        public int State { get; set; }
        public string LocationId { get; set; }

        public User Admin { get; set; }
        public Asset Asset { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public ReturnRequest ReturnRequest { get; set; }
    }
}
