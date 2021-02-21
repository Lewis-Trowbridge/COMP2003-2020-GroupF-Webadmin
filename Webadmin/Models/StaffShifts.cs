using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Webadmin.Models
{
    public partial class StaffShifts
    {
        public int StaffShiftId { get; set; }
        public int? StaffId { get; set; }
        public DateTime StaffStartTime { get; set; }
        public DateTime StaffEndTime { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
