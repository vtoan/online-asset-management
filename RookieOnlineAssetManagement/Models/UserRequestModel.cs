using RookieOnlineAssetManagement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class UserRequestModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public bool? Gender { get; set; }
        [Required]
        public DateTime? JoinedDate { get; set; }
        public TypeUser Type { get; set; }
        public string LocationId { get; set; }
    }
}
