using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;
using Microsoft.Data.SqlClient;

namespace Webadmin.Controllers
{
    public class FlagsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public FlagsController(COMP2003_FContext context)
        {
            _context = context;
        }


        //-- create new flag --
        public async Task<IActionResult> CreateFlag()
        {
            return View("FlagForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFlag(string flagTitle, string flagLocationPage, string flagCategory, 
            bool flagPersistent, int flagUrgency, string flagDesc, int flagVenueId, DateTime flagDate, bool flagResolved)
        {
            ViewResult result = await CallAddFlagSP(flagTitle, flagLocationPage, flagCategory, flagPersistent, flagUrgency, flagDesc, flagVenueId, flagDate, flagResolved);
            
            //TODO: Replace this with returning of the dashboard view once implemented
            return result;
        }
        private async Task<ViewResult> CallAddFlagSP(string flagTitle, string flagLocationPage, string flagCategory,
            bool flagPersistent, int flagUrgency, string flagDesc, int flagVenueId, DateTime flagDate, bool flagResolved)
        {
            // Initialisation of parameters - long and monotonous but necessary
            SqlParameter[] parameters = new SqlParameter[9];

            //parameters[0] = new SqlParameter("@id", 1); //done through identity server side, might take out

            parameters[0] = new SqlParameter("@flag_title", flagTitle);
            parameters[1] = new SqlParameter("@flag_location_page", flagLocationPage);
            parameters[2] = new SqlParameter("@flag_category", flagCategory);
            parameters[3] = new SqlParameter("@flag_persistent", flagPersistent);
            parameters[4] = new SqlParameter("@flag_urgency", flagUrgency);
            parameters[5] = new SqlParameter("@flag_desc", flagDesc);
            parameters[6] = new SqlParameter("@flag_venue_id", flagVenueId);
            parameters[7] = new SqlParameter("@flag_date", flagDate);
            parameters[8] = new SqlParameter("@flag_resolved", flagResolved);          



            // Executes the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC add_error @flag_title, @flag_location_page, @flag_category, @flag_persistent, @flag_urgency, @flag_desc, @flag_venue_id, @flag_date, @flag_resolved", parameters);

            //return the id of the flag just created - set to 1 for now but change **
            return View("FlagForm");
        }
        //-- /create new flag --
    }
}
