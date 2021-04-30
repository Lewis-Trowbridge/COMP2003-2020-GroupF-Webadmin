using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Webadmin.Requests
{
    public class DeleteRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
