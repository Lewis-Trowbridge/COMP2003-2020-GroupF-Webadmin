using System;
using System.Collections.Generic;

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
        public string AdminSalt { get; set; }
        public string AdminLevel { get; set; }

        public virtual ICollection<AdminLocations> AdminLocations { get; set; }
    }
}
