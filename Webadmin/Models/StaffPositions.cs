using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class StaffPositions
    {
        public StaffPositions()
        {
            Staff = new HashSet<Staff>();
        }

        public int StaffPositionId { get; set; }
        public string StaffPositionName { get; set; }

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
