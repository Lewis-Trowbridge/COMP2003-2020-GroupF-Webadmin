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

        public static AdminLocations GetTestAdminLocation(Venues venue, Admins admin)
        {
            return new AdminLocations
            {
                Admin = admin,
                Venue = venue
            };
        }

        public static Venues GetTestVenue(int id)
        {
            return new Venues
            {
                VenueId = id,
                VenueName = "Test Venue",
                AddLineOne = "Test line one",
                AddLineTwo = "Test line two",
                County = "Test county",
                City = "Test city",
                VenuePostcode = "PL4 8AA"
            };
        }
    }
}
