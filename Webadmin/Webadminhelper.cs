using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Webadmin.Models;

namespace Webadmin
{
    public class WebadminHelper
    {
        public const string AdminIdKey = "_adminId";
        public const string StaffIdKey = "_staffId";

        public static int? GetAdminId(ISession sessionContext)
        {
            return sessionContext.GetInt32(AdminIdKey);
        }

        public static bool AdminPermissionVenue(ISession sessionContext, int venueId, COMP2003_FContext dbContext)
        {
            int? adminId = GetAdminId(sessionContext);
            if (adminId != null)
            {
                bool exists = dbContext.Admins
                    .Where(admin => admin.AdminId.Equals(adminId))
                    .Join(dbContext.AdminLocations, admin => admin.AdminId, location => location.AdminId, (admin, location) => new
                    {
                        Admin = admin,
                        Location = location
                    })
                    .Any(location => location.Location.VenueId.Equals(venueId));
                // If we can find the venue ID in the admin's locations, then they have permission, if we can't, they don't
                return exists;
            }
            // If admin ID is not set, then no permission should be given
            return false;
            
        }

        public static bool AdminPermissionStaff(ISession sessionContext, int staffId, COMP2003_FContext dbContext)
        {
            int? adminId = GetAdminId(sessionContext);
            if (adminId != null)
            {
                bool exists = dbContext.Admins
                    // Filter out all other admins, ensures that we return false when admin ID is not set
                    .Where(admin => admin.AdminId.Equals(adminId))
                    // Join admins with admin_locations on admin ID
                    .Join(dbContext.AdminLocations, admin => admin.AdminId, location => location.AdminId, (admin, location) => new
                    {
                        Admin = admin,
                        Location = location
                    })
                    // Join location with employment on venue ID
                    .Join(dbContext.Employment, adminAndLocation => adminAndLocation.Location.Venue.VenueId, employment => employment.VenueId, (adminAndLocation, employment) => new
                    {
                        Employment = employment
                    })
                    // Return true if the member of staff is employed at any of the venues that the current admin manages, if not returns false
                    .Any(employment => employment.Employment.StaffId.Equals(staffId));
                return exists;
            }
            return false;
        }

        public static bool StaffIsClockedIn(Staff staffToCheck)
        {
            bool clockedIn = staffToCheck.StaffShifts
                .Any(shift => shift.StaffEndTime.Equals(default(DateTime)));
            return clockedIn;
        }
    }
}
