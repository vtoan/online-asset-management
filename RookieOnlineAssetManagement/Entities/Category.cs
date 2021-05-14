using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Entities
{
    public class Category
    {
        public Category()
        {
            Assets = new HashSet<Asset>();
        }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ShortName { get; set; }
        public int? NumIncrease { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
