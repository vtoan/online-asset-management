using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetDetailModel : AssetModel
    {
        public string Specification { get; set; }
        public DateTime InstalledDate { get; set; }
        public string LocationName { get; set; }
        public string LocationId { get; set; }
        public string CategoryId { get; set; }
    }
}
