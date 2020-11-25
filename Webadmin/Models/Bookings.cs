using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class Bookings
    {
        public Bookings()
        {
            BookingLocations = new HashSet<BookingLocations>();
        }

        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public int BookingSize { get; set; }

        public virtual ICollection<BookingLocations> BookingLocations { get; set; }
    }
}
