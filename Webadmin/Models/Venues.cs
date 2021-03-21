using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class Venues
    {
        public Venues()
        {
            AdminLocations = new HashSet<AdminLocations>();
            BookingLocations = new HashSet<BookingLocations>();
            Employment = new HashSet<Employment>();
            VenueTables = new HashSet<VenueTables>();
        }

        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenuePostcode { get; set; }
        public string AddLineOne { get; set; }
        public string AddLineTwo { get; set; }
        public string City { get; set; }
        public string County { get; set; }

        public virtual ICollection<AdminLocations> AdminLocations { get; set; }
        public virtual ICollection<BookingLocations> BookingLocations { get; set; }
        public virtual ICollection<Employment> Employment { get; set; }
        public virtual ICollection<VenueTables> VenueTables { get; set; }
    }
}
