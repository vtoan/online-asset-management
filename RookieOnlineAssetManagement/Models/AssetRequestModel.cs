using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetRequestModel
    {
        public string AssetId { get; set; }
        public string CategoryName { get; set; }
        public string ShortName { get; set; }
        public string AssetName { get; set; }
        public int State { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string Location { get; set; }
    }
}
