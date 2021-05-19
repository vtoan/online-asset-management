using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetModel
    {
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public string CategoryName { get; set; }
        public int State { get; set; }
        
    }
}
