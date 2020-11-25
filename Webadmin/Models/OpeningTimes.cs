using System;
using System.Collections.Generic;

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
