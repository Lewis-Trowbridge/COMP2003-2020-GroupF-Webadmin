using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class WebBookingsView
    {
        [Index(0)]
        public string CustomerName { get; set; }
        [Index(1)]
        public string CustomerContactNumber { get; set; }
        [Index(2)]
        public DateTime BookingTime { get; set; }
        [Index(3)]
        public string StaffName { get; set; }
    }
}
