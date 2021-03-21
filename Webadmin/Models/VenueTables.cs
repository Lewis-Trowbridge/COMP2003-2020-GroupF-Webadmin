using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Webadmin.Models
{
    public partial class VenueTables
    {
        [Key]
        public int VenueTableId { get; set; }
        public int VenueId { get; set; }
        public int VenueTableNum { get; set; }
        public int VenueTableCapacity { get; set; }

        public virtual Venues Venue { get; set; }
    }
}
