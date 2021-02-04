using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Employment = new HashSet<Employment>();
            StaffShifts = new HashSet<StaffShifts>();
        }

        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int StaffContactNum { get; set; }
        public int? StaffPositionId { get; set; }

        public virtual StaffPositions StaffPosition { get; set; }
        public virtual ICollection<Employment> Employment { get; set; }
        public virtual ICollection<StaffShifts> StaffShifts { get; set; }
    }
}
