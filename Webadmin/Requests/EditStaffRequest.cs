using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class EditStaffRequest
    {
        [Required]
        public int StaffId { get; set; }

        [Required]
        public int VenueId { get; set; }

        [StringLength(50)]
        public string StaffName { get; set; }

        [StringLength(15)]
        public string StaffContactNum { get; set; }

        [StringLength(20)]
        public string StaffPosition { get; set; }
    }
}
