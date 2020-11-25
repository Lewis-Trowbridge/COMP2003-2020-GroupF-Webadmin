using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class Admin
    {
        public Admin()
        {
            AdminLocations = new HashSet<AdminLocation>();
        }

        public int AdminId { get; set; }
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public string AdminLevel { get; set; }

        public virtual ICollection<AdminLocation> AdminLocations { get; set; }
    }
}
