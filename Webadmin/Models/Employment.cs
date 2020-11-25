using System;
using System.Collections.Generic;

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
