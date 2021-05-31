using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestModel
    {
        public string AssetId { get; set; }

        [Required(ErrorMessage = "Category cannot null")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage = "Asset Name cannot null")]
        public string AssetName { get; set; }

        [RegularExpression(@"^\d$")]
        public short State { get; set; }

        [Required(ErrorMessage = "Specification cannot null")]
        public string Specification { get; set; }

        [Required(ErrorMessage = "Installed Date cannot null")]
        public DateTime? InstalledDate { get; set; }
        [BindNever]
        public string LocationId { get; set; }
    }
}
