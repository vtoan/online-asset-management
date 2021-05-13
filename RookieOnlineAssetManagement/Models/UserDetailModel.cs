using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class UserDetailModel :UserModel
    {
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Location { get; set; }
    }
}
