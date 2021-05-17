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
        [Required(ErrorMessage ="First Name cannot null")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name cannot null")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth cannot null")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender cannot null")]
        public bool? Gender { get; set; }

        [Required(ErrorMessage = "Joined Date cannot null")]
        public DateTime? JoinedDate { get; set; }

        [Required(ErrorMessage = "Type cannot null")]
        public TypeUser Type { get; set; }
        public string LocationId { get; set; }
    }
}
