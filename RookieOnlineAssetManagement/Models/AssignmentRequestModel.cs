using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RookieOnlineAssetManagement.Models
{
    public class AssignmentRequestModel
    {
        public string AssignmentId { get; set; }

        [Required(ErrorMessage = "UserId cannot null")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "AssetId cannot null")]
        public string AssetId { get; set; }

        // [Required(ErrorMessage ="AdminId cannot null")]
        [BindNever]
        public string AdminId { get; set; }

        [Required(ErrorMessage = "Assigned Date cannot null")]
        public DateTime? AssignedDate { get; set; }

        [Required(ErrorMessage = "Note cannot null")]
        public string Note { get; set; }

        // [Required(ErrorMessage ="LocationId cannot null")]
        [BindNever]
        public string LocationId { get; set; }
        public int State { get; set; }
    }
}
