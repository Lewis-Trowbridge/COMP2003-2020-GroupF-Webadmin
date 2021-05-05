using System;
using System.Collections.Generic;
using System.Text;
using Webadmin.Models;
using Webadmin.Requests;

namespace Webadmin.Tests.Helpers
{
    class StaffControllerTestHelper
    {
        public static Staff GetTestStaff(int id)
        {
            return new Staff
            {
                StaffId = id,
                StaffName = "Test staff",
                StaffContactNum = "07331239109",
                StaffPosition = "Test"
            };
        }

        public static CreateStaffRequest GetCreateStaffRequest(Staff staff, Venues venue)
        {
            return new CreateStaffRequest
            {
                VenueId = venue.VenueId,
                StaffName = staff.StaffName,
                StaffContactNum = staff.StaffContactNum,
                StaffPosition = staff.StaffPosition
            };
        }
    }
}
