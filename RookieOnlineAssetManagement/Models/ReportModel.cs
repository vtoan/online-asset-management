using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RookieOnlineAssetManagement.Models
{
    public class ReportModel
    {
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Total")]
        public int Total { get; set; }

        [DisplayName("Assigned Total")]
        public int AssignedTotal { get; set; }

        [DisplayName("Available Total")]
        public int AvailableTotal { get; set; }

        [DisplayName("Not Available Total")]
        public int NotAvailableTotal { get; set; }

        [DisplayName("Wating For Recycling Total")]
        public int WatingRecyclingTotal { get; set; }

        [DisplayName("Recycled Total")]
        public int RecycledTotal { get; set; }
    }
}
