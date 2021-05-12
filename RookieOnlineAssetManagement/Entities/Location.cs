using System.Collections.Generic;

namespace RookieOnlineAssetManagement.Entities
{
    public class Location
    {
        public Location()
        {
            Assets = new HashSet<Asset>();
            Assignments = new HashSet<Assignment>();
            Users = new HashSet<User>();
        }
        public string LocationId { get; set; }
        public string LocationName { get; set; }

        public ICollection<Asset> Assets { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
