using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class BookingLocation
    {
        public int VenueId { get; set; }
        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
