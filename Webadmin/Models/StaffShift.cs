using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class StaffShift
    {
        public int StaffShiftId { get; set; }
        public int? StaffId { get; set; }
        public DateTime StaffStartTime { get; set; }
        public DateTime StaffEndTime { get; set; }

        public virtual staff Staff { get; set; }
    }
}
