using System;
using System.Collections.Generic;
using System.Text;
using Webadmin.Models;
using Webadmin.Requests;

namespace Webadmin.Tests.Helpers
{
    class AdminsControllerTestHelper
    {
        public static CreateAdminRequest GetCreateAdminRequest(Admins admin)
        {
            CreateAdminRequest request = new CreateAdminRequest
            {
                AdminUsername = admin.AdminUsername,
                AdminPassword = admin.AdminPassword
            };
            return request;
        }
    }
}
