using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class Venue
    {
        public Venue()
        {
            AdminLocations = new HashSet<AdminLocation>();
            BookingLocations = new HashSet<BookingLocation>();
            Employments = new HashSet<Employment>();
        }

        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenuePostcode { get; set; }
        public string AddLineOne { get; set; }
        public string AddLineTwo { get; set; }
        public string City { get; set; }
        public string County { get; set; }

        public virtual ICollection<AdminLocation> AdminLocations { get; set; }
        public virtual ICollection<BookingLocation> BookingLocations { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
    }
}
