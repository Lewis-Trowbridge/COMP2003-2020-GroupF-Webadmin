using System;
using System.Collections.Generic;

namespace Webadmin.Models
{
    public partial class Customers
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerContactNumber { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerPassword { get; set; }
    }
}
