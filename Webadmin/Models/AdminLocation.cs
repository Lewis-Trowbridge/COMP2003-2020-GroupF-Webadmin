using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class AdminLocation
    {
        public int VenueId { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
