using System;

namespace RookieOnlineAssetManagement.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string StaffCode { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string RoleName { get; set; }
        public string LocationId { get; set; }
        public bool Status { get; set; }
    }
}
