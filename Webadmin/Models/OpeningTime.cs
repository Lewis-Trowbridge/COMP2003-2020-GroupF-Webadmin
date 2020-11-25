using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class OpeningTime
    {
        public int VenueTimeId { get; set; }
        public int VenueId { get; set; }
        public TimeSpan VenueOpeningTime { get; set; }
        public TimeSpan VenueClosingTime { get; set; }
    }
}
