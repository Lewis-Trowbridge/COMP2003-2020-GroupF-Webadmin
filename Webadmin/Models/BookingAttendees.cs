using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class BookingAttendees
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public bool BookingAttended { get; set; }
    }
}
