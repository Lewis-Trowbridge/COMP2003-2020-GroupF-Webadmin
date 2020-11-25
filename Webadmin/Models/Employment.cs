using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class Employment
    {
        public int VenueId { get; set; }
        public int StaffId { get; set; }

        public virtual staff Staff { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
