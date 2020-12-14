using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;

namespace Webadmin.Data
{
    public class WebadminContext : DbContext
    {
        public WebadminContext (DbContextOptions<WebadminContext> options)
            : base(options)
        {
        }

        public DbSet<Webadmin.Models.Flags> Flags { get; set; }
    }
}
