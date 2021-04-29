using System;
using System.Collections.Generic;
using System.Text;
using Webadmin.Models;
using Webadmin.Requests;

namespace Webadmin.Tests.Helpers
{
    class VenuesControllerTestHelper
    {
        public static CreateVenueRequest GetCreateVenueRequest()
        {
            return new CreateVenueRequest
            {
                VenueName = "Test Venue",
                VenueAddLineOne = "Test line one",
                VenueAddLineTwo = "Test line two",
                VenueCounty = "Test county",
                VenueCity = "Test city",
                VenuePostcode = "PL4 8AA"
            };
        }
    }
}
