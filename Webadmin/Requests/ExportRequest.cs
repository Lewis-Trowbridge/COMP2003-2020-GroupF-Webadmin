using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class ExportRequest
    {
        [Required]
        public int VenueId { get; set; }
        [Required]
        public DateTime ExportFrom { get; set; }
        [Required]
        public DateTime ExportTo { get; set; }
    }
}
