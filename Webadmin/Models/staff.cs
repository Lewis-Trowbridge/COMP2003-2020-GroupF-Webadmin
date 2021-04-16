using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Bookings = new HashSet<Bookings>();
            Employment = new HashSet<Employment>();
            StaffShifts = new HashSet<StaffShifts>();
        }

        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffContactNum { get; set; }
        public string StaffPosition { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; }
        public virtual ICollection<Employment> Employment { get; set; }
        public virtual ICollection<StaffShifts> StaffShifts { get; set; }
    }
}
