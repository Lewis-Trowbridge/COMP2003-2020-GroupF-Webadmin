using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class staff
    {
        public staff()
        {
            Employments = new HashSet<Employment>();
            StaffShifts = new HashSet<StaffShift>();
        }

        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public int StaffContactNum { get; set; }
        public int? StaffPositionId { get; set; }

        public virtual StaffPosition StaffPosition { get; set; }
        public virtual ICollection<Employment> Employments { get; set; }
        public virtual ICollection<StaffShift> StaffShifts { get; set; }
    }
}
