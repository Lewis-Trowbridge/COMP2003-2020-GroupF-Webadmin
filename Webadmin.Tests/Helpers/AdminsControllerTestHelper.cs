using System;
using System.Collections.Generic;
using System.Text;
using Webadmin.Models;

namespace Webadmin.Tests.Helpers
{
    class AdminsControllerTestHelper
    {
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
