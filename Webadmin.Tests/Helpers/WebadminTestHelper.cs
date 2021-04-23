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
    }
}
