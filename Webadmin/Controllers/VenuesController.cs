using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Webadmin.Models;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using Webadmin.Requests;

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
        public async Task<IActionResult> Index()
        {

            var adminId = WebadminHelper.GetAdminId(HttpContext.Session);
            return View(await _context.Venues
                .Join(_context.AdminLocations, venue => venue.VenueId, location => location.VenueId, (venue, location) => new
                {
                    Location = location,
                    Venue = venue
                })
                .Where(venueAndLocation => venueAndLocation.Location.AdminId.Equals(adminId))
                .Select(venue => venue.Venue)
                .ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, id.Value, _context))
            {
                var venues = await _context.Venues
                    .FirstOrDefaultAsync(m => m.VenueId == id);
                if (venues == null)
                {
                    return NotFound();
                }

                return View(venues);
        }
            return Unauthorized();
    }

        /*   NON GENERATED CODE   */

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVenueRequest request)
        {
            int adminId = WebadminHelper.GetAdminId(HttpContext.Session).Value;
            int venueId = await CallAddVenueSP(request.VenueName, request.VenueAddLineOne, request.VenueAddLineTwo, request.VenueCity, request.VenueCounty, request.VenuePostcode, adminId);
            return RedirectToAction(nameof(Index), new { id = venueId });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, id, _context))
            {
                ViewBag.venueId = id;
                var venues = await _context.Venues
                .Where(venue => venue.VenueId.Equals(id))
                .Select(venue => new EditVenueRequest
                {
                    VenueId = venue.VenueId,
                    VenueName = venue.VenueName,
                    VenueAddLineOne = venue.AddLineOne,
                    VenueAddLineTwo = venue.AddLineTwo,
                    VenueCity = venue.City,
                    VenueCounty = venue.County,
                    VenuePostcode = venue.VenuePostcode
                })
                .SingleAsync();
                if (venues == null)
                {
                    return NotFound();
                }

                return View(venues);
            }

            return Unauthorized();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditVenueRequest request)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, request.VenueId, _context))
            {
                await CallEditVenueSP(request.VenueName, request.VenueAddLineOne, request.VenueAddLineTwo, request.VenueCity, request.VenueCounty, request.VenuePostcode, request.VenueId);
            return RedirectToAction(nameof(Index));
            }

            return Unauthorized();
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, id.Value, _context))
            {
                var venues = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venues == null)
            {
                return NotFound();
            }

            return View(venues);
            }

            return Unauthorized();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int venueId)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, venueId, _context))
            {
                await CallDeteteVenueSP(venueId);
                return RedirectToAction(nameof(Index));
            }

            return Unauthorized();
        }

        [HttpGet]
        public IActionResult Export(int venueId)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, venueId, _context))
            {
                ViewBag.venueId = venueId;
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Export(ExportRequest request)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, request.VenueId, _context))
            {
                using var stream = new StringWriter();
                using var writer = new CsvWriter(stream, CultureInfo.InvariantCulture);

                var views = await _context.WebBookingsView
                    .Where(view => view.VenueId.Equals(request.VenueId))
                    .Where(view => view.BookingTime.Date >= request.ExportFrom.Date)
                    .Where(view => view.BookingTime.Date <= request.ExportTo.Date)
                    .ToListAsync();

                await writer.WriteRecordsAsync(views);

                byte[] csvBytes = Encoding.Unicode.GetBytes(stream.ToString());

                return File(csvBytes, "text/csv", "export.csv");
            }

            else
            {
                return Unauthorized();
            }

        }


        /* DATABASE LINKED CODE */

        private async Task<int> CallAddVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int adminId)
        {
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

        private async Task CallEditVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int venueId)
        {
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@venue_name", venueName);
            parameters[1] = new SqlParameter("@add_line_one", venueAddressLineOne);
            parameters[2] = new SqlParameter("@add_line_two", venueAddressLineTwo);
            parameters[3] = new SqlParameter("@venue_postcode", venuePostcode);
            parameters[4] = new SqlParameter("@city", venueCity);
            parameters[5] = new SqlParameter("@county", venueCounty);
            parameters[6] = new SqlParameter("@venue_id", venueId);
            await _context.Database.ExecuteSqlRawAsync("EXEC edit_venue @venue_id, @venue_name, @venue_postcode, @add_line_one, @add_line_two, @city, @county", parameters);
        }

        private async Task CallDeteteVenueSP(int venueId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_id", venueId);
            await _context.Database.ExecuteSqlRawAsync("EXEC delete_venue @venue_id", parameters);
        }

        /*   GENERATED CODE   */

        private bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
