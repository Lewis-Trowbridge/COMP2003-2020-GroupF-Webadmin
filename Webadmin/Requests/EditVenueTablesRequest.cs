using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class EditVenueTablesRequest
    {
        [Required]
        public int VenueId { get; set; }
        [Required]
        public int VenueTableId { get; set; }
        public int VenueTableNum { get; set; }
        public int VenueTableCapacity { get; set; }
    }
}
