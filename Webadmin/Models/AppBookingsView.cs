﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class AppBookingsView
    {
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public int BookingSize { get; set; }
        public bool BookingAttended { get; set; }
        public int CustomerId { get; set; }
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenuePostcode { get; set; }
        public int VenueTableId { get; set; }
        public int VenueTableNum { get; set; }
        public int VenueTableCapacity { get; set; }
        public string AddLineOne { get; set; }
        public string AddLineTwo { get; set; }
        public string City { get; set; }
        public string County { get; set; }
    }
}
