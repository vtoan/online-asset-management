using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReportModel
    {
        public string CategoryName { get; set; }
        public int Total { get; set; }
        public int AssignedTotal { get; set; }
        public int AvailableTotal { get; set; }
        public int NotAvailableTotal { get; set; }
        public int WatingRecyclingTotal { get; set; }
        public int RecycledTotal { get; set; }
    }
}
