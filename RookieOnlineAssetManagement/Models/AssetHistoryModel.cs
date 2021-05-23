using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class AssetHistoryModel
    {
        public string AssignmentId { get; set; }
        public DateTime Date { get; set; }
        public string  AssignedTo { get; set; }
        public string AssignedBy { get; set; }
        public DateTime ReturnedDate { get; set; }
    }
}
