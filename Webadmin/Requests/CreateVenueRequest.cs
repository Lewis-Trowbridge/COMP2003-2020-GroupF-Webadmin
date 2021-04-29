using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class CreateVenueRequest
    {
        [Required]
        [StringLength(50)]
        public string VenueName { get; set; }
        [Required]
        [StringLength(10)]
        public string VenuePostcode { get; set; }
        [Required]
        [StringLength(100)]
        public string VenueAddLineOne { get; set; }
        [Required]
        [StringLength(100)]
        public string VenueAddLineTwo { get; set; }
        [Required]
        [StringLength(50)]
        public string VenueCity { get; set; }
        [Required]
        [StringLength(50)]
        public string VenueCounty { get; set; }
    }
}
