using System;
using System.Collections.Generic;

#nullable disable

namespace Webadmin.Models
{
    public partial class StaffPosition
    {
        public StaffPosition()
        {
            staff = new HashSet<staff>();
        }

        public int StaffPositionId { get; set; }
        public string StaffPositionName { get; set; }

        public virtual ICollection<staff> staff { get; set; }
    }
}
