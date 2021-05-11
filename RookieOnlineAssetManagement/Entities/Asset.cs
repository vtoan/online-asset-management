using System;
using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Entities
{
    public class Asset
    {
        public Asset()
        {
            Assignments = new HashSet<Assignment>();
        }
        public string AssetId { get; set; }
        public string CategoryId { get; set; }
        public string AssetName { get; set; }
        public short State { get; set; }
        public string Specification { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string LocationId { get; set; }

        public Category Category { get; set; }
        public Location Location { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
