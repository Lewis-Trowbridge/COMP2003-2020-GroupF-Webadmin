using System;
using System.Collections.Generic;

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
