using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class Admins
    {
        public Admins()
        {
            AdminLocations = new HashSet<AdminLocations>();
        }

        public int AdminId { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }

        public virtual ICollection<AdminLocations> AdminLocations { get; set; }
    }
}
