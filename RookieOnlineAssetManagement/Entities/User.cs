using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            AssignmentAdmins = new HashSet<Assignment>();
            AssignmentUsers = new HashSet<Assignment>();
            ReturnRequestAcceptedUsers = new HashSet<ReturnRequest>();
            ReturnRequestRequestUsers = new HashSet<ReturnRequest>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public DateTime? JoinedDate { get; set; }
        public bool? IsChange { get; set; }
        public string LocationId { get; set; }
        public string StaffCode { get; set; }
        public int NumIncrease { get; set; }

        public Location Location { get; set; }
        public ICollection<Assignment> AssignmentAdmins { get; set; }
        public ICollection<Assignment> AssignmentUsers { get; set; }
        public ICollection<ReturnRequest> ReturnRequestAcceptedUsers { get; set; }
        public ICollection<ReturnRequest> ReturnRequestRequestUsers { get; set; }
        public ICollection<IdentityRole> Roles { get; set; }
    }
}
