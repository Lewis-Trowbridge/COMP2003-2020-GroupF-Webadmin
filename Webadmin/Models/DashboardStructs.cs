using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webadmin.Models
{
    public class DashboardStructs
    {
        public struct BookingDashboardDisplay
        {
            public int BookingId { get; set; }
            public DateTime BookingTime { get; set; }
            public int BookingSize { get; set; }
            public bool BookingAttended { get; set; }
            public string BookingCustomerName { get; set; }
        }

        public struct StaffDashboardDisplay
        {
            public int StaffId { get; set; }
            public string StaffName { get; set; }
            public int StaffContactNum { get; set; }
            public string StaffPosition { get; set; }
        }
    }
}
