using System;

namespace RookieOnlineAssetManagement.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public DateTime? JoinedDate { get; set; }
    }
}
