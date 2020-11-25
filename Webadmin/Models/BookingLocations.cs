using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class BookingLocations
    {
        public int VenueId { get; set; }
        public int BookingId { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Venues Venue { get; set; }
    }
}
