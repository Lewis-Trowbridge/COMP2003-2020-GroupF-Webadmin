using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class VenueTables
    {
        public int VenueTableId { get; set; }
        public int VenueId { get; set; }
        public int VenueTableNum { get; set; }
        public int VenueTableCapacity { get; set; }
    }
}
