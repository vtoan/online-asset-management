using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class UserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public DateTime? JoinedDate { get; set; }
        public TypeUser Type { get; set; }
        public string LocationId { get; set; }
    }
}
