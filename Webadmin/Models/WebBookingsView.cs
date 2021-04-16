using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class WebBookingsView
    {
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public DateTime BookingTime { get; set; }
        public string StaffName { get; set; }
    }
}
