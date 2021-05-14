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
        [Required]
        [StringLength(50)]
        public string VenueName { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z][A-HJ-Y]?\d\d?\s?[A-Zz\d]??\d[A-Z]{2}|GIR ?0A{2})$", ErrorMessage = "Please input a valid postcode.")]
        [StringLength(10)]
        public string VenuePostcode { get; set; }
        [Required]
        [StringLength(100)]
        public string VenueAddLineOne { get; set; }
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
