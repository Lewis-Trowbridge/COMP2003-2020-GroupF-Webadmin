using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class OpeningTimes
    {
        public int VenueTimeId { get; set; }
        public int VenueId { get; set; }
        public TimeSpan VenueOpeningTime { get; set; }
        public TimeSpan VenueClosingTime { get; set; }
    }
}
