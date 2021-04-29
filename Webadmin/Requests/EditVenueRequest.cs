using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class EditVenueRequest
    {
        [Required]
        public int VenueId { get; set; }
        [StringLength(50)]
        public string VenueName { get; set; }
        [StringLength(10)]
        public string VenuePostcode { get; set; }
        [StringLength(100)]
        public string VenueAddLineOne { get; set; }
        [StringLength(100)]
        public string VenueAddLineTwo { get; set; }
        [StringLength(50)]
        public string VenueCity { get; set; }
        [StringLength(50)]
        public string VenueCounty { get; set; }
    }
}
