using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Webadmin.Models;

namespace Webadmin.Controllers
{
    public class VenuesController : Controller
    {
        private readonly COMP2003_FContext _context;

        public VenuesController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index(int id)
        {
            // Set admin ID - hardcoded temporarily
            //_context.Interceptor.SetAdminId(1);
            ViewBag.adminId = id;
            return View(await _context.Venues.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
        }

        /*   NON GENERATED CODE   */

        public IActionResult Create(int id)
        {
            ViewBag.adminId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string venueName, string venuePostcode, string addLineOne, string addLineTwo, string city, string county, int adminId)
        {
            int venueId = await CallAddVenueSP(venueName, addLineOne, addLineTwo, city, county, venuePostcode, adminId);
            return RedirectToAction(nameof(Index), new { id = venueId });
        }

        public IActionResult Edit(int id)
        {
            ViewBag.venueId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string venueName, string addLineOne, string addLineTwo, string city, string county, string venuePostcode, int venueId)
        {
            CallEditVenueSP(venueName, addLineOne, addLineTwo, city, county, venuePostcode, venueId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
        }
        [HttpPost]
        public IActionResult Delete(int venueId)
        {
            CallDeteteVenueSP(venueId);
            return RedirectToAction(nameof(Index));
        }


        /* DATABASE LINKED CODE */

        private async Task<int> CallAddVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int adminId)
        {
            //TODO: Replace this once retrieval of admin ID is possible
            //adminId = 1;
            // Initialisation of parameters - long and monotonous but necessary
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@venue_name", venueName);
            parameters[1] = new SqlParameter("@add_line_one", venueAddressLineOne);
            parameters[2] = new SqlParameter("@add_line_two", venueAddressLineTwo);
            parameters[3] = new SqlParameter("@venue_postcode", venuePostcode);
            parameters[4] = new SqlParameter("@city", venueCity);
            parameters[5] = new SqlParameter("@county", venueCounty);
            parameters[6] = new SqlParameter("@admin_id", adminId);
            // This is key - this is how we will receive the ID of the newly-created venue
            parameters[7] = new SqlParameter("@venue_id", 456);
            parameters[7].Direction = System.Data.ParameterDirection.Output;

            // Executes the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC add_venue @venue_name, @add_line_one, @add_line_two, @venue_postcode, @city, @county, @admin_id, @venue_id OUTPUT", parameters);

            return (int)parameters[7].Value;
        }

        private void CallEditVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int venueId)
        {
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@venue_name", venueName);
            parameters[1] = new SqlParameter("@add_line_one", venueAddressLineOne);
            parameters[2] = new SqlParameter("@add_line_two", venueAddressLineTwo);
            parameters[3] = new SqlParameter("@venue_postcode", venuePostcode);
            parameters[4] = new SqlParameter("@city", venueCity);
            parameters[5] = new SqlParameter("@county", venueCounty);
            parameters[6] = new SqlParameter("@venue_id", venueId);
            _context.Database.ExecuteSqlRaw("EXEC edit_venue @venue_id, @venue_name, @venue_postcode, @add_line_one, @add_line_two, @city, @county", parameters);
        }

        private void CallDeteteVenueSP(int venueId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_id", venueId);
            _context.Database.ExecuteSqlRaw("EXEC delete_venue @venue_id", parameters);
        }

        /*   GENERATED CODE   */

        private bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
