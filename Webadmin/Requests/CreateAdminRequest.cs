using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class CreateAdminRequest
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string AdminUsername { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string AdminPassword { get; set; }
    }
}
