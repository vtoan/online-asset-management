using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestModel
    {
        public string AssetId { get; set; }

        [Required(ErrorMessage ="Category cannot null")]
        public string CategoryId { get; set; }

        [Required(ErrorMessage ="Asset Name cannot null")]
        public string AssetName { get; set; }

        public short State { get; set; }

        [Required(ErrorMessage = "Specification cannot null")]
        public string Specification { get; set; }

        [Required(ErrorMessage = "Installed Date cannot null")]
        public DateTime? InstalledDate { get; set; }

        public string LocationId { get; set; }
    }
}
