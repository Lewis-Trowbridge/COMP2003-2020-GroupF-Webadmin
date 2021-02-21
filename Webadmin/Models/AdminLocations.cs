using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
