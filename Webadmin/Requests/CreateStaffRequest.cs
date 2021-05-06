using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class CreateStaffRequest
    {
        [Required]
        public int VenueId { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffName { get; set; }

        [Required]
        [StringLength(15)]
        public string StaffContactNum { get; set; }

        [Required]
        [StringLength(20)]
        public string StaffPosition { get; set; }
    }
}
