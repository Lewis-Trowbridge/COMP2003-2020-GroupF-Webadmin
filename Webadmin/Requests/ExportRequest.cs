using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webadmin.Requests
{
    public class ExportRequest
    {
        public int VenueId { get; set; }
        public DateTime ExportFrom { get; set; }
        public DateTime ExportTo { get; set; }
    }
}
