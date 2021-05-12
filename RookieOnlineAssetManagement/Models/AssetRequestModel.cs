using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestModel
    {
        public string AssetId { get; set; }
        public string CategoryId { get; set; }
        public string AssetName { get; set; }
        public short State { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string LocationId { get; set; }
    }
}
