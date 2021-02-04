using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class AdminLocations
    {
        public int VenueId { get; set; }
        public int AdminId { get; set; }

        public virtual Admins Admin { get; set; }
        public virtual Venues Venue { get; set; }
    }
}
