using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class BookingAttendee
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public bool BookingAttended { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
