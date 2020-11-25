using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingLocations = new HashSet<BookingLocation>();
        }

        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public int BookingSize { get; set; }

        public virtual ICollection<BookingLocation> BookingLocations { get; set; }
    }
}
