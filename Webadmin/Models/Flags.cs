using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class Flags
    {
        public int Id { get; set; }
        public string FlagTitle { get; set; }
        public string FlagLocationPage { get; set; }
        public string FlagCategory { get; set; }
        public bool? FlagPersistent { get; set; }
        public int? FlagUrgency { get; set; }
        public string FlagDesc { get; set; }
        public int? FlagVenueId { get; set; }
        public DateTime? FlagDate { get; set; }
        public bool? FlagResolved { get; set; }
    }
}
