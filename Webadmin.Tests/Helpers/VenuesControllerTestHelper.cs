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

        public static EditVenueRequest GetEditVenueRequest(Venues venue)
        {
            return new EditVenueRequest
            {
                VenueId = venue.VenueId,
                VenueName = venue.VenueName + "ed",
                VenueAddLineOne = venue.AddLineOne + "ed",
                VenueAddLineTwo = venue.AddLineTwo + "ed",
                VenueCity = venue.City + "ed",
                VenueCounty = venue.County + "ed",
                VenuePostcode = "ed"
            };
        }

        public static DeleteRequest GetDeleteVenueRequest(Venues venue)
        {
            return new DeleteRequest
            {
                Id = venue.VenueId
            };
        }
    }
}
