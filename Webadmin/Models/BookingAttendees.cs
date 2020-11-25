using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class BookingAttendees
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public bool BookingAttended { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
