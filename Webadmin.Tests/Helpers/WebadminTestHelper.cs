using System;
using System.Collections.Generic;
using System.Text;
using Webadmin.Models;

namespace Webadmin.Tests.Helpers
{
    class WebadminTestHelper
    {
        public static COMP2003_FContext GetDbContext()
        {
            var dbContext = new COMP2003_FContext();
            return dbContext;
        }

        public static Admins GetTestAdmin(int id)
        {
            Admins admin = new Admins
            {
                AdminId = id,
                AdminUsername = "testadmin01",
                AdminPassword = "testpassword"
            };
            return admin;
        }
    }
}
