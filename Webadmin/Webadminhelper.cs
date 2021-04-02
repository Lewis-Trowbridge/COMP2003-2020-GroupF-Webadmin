using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Webadmin.Models;

namespace Webadmin
{
    public class Webadminhelper
    {
        public static string AdminIdKey = "_adminId";

        public static bool AdminPermissionVenue(ISession sessionContext, int venueId, COMP2003_FContext dbContext)
        {
            int? adminId = sessionContext.GetInt32(AdminIdKey);
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
    }
}
