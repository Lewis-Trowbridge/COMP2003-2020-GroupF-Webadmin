using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class Employment
    {
        public int VenueId { get; set; }
        public int StaffId { get; set; }

        public virtual Staff Staff { get; set; }
        public virtual Venues Venue { get; set; }
    }
}
