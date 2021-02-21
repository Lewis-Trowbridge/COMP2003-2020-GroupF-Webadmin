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
        private readonly cleanTableDbContext _context;

        public VenuesController(cleanTableDbContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            // Set admin ID - hardcoded temporarily
            _context.Interceptor.SetAdminId(1);
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

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string venueName, string venuePostcode, string addLineOne, string addLineTwo, string city, string county, int adminId)
        {
            int venueId = await CallAddVenueSP(venueName, addLineOne, addLineTwo, city, county, venuePostcode, adminId);
            return RedirectToAction(nameof(Dashboard), new { id = venueId });
        }

        public async Task<IActionResult> Dashboard(int? id)
        {
            _context.Interceptor.SetAdminId(1);

            Venues venueToDisplay = await _context.Venues.FindAsync(id);


            if (venueToDisplay != null)
            {

                // Grab necessary data for bookings from database
                List<DashboardStructs.BookingDashboardDisplay> bookingsList = await (from venue in _context.Venues
                                                                                     join location in _context.BookingLocations on venue.VenueId equals location.VenueId
                                                                                     join booking in _context.Bookings on location.BookingId equals booking.BookingId
                                                                                     join bookingAttendees in _context.BookingAttendees on booking.BookingId equals bookingAttendees.BookingId
                                                                                     join customers in _context.Customers on bookingAttendees.CustomerId equals customers.CustomerId
                                                                                     select new DashboardStructs.BookingDashboardDisplay
                                                                                     {
                                                                                         BookingId = booking.BookingId,
                                                                                         BookingTime = booking.BookingTime,
                                                                                         BookingSize = booking.BookingSize,
                                                                                         BookingAttended = bookingAttendees.BookingAttended,
                                                                                         BookingCustomerName = customers.CustomerName
                                                                                     }).Take(5).ToListAsync();

                // Grab necessary data for staff from database
                List<DashboardStructs.StaffDashboardDisplay> staffList = await (from venue in _context.Venues
                                                                                join employment in _context.Employment on venue.VenueId equals employment.VenueId
                                                                                join staff in _context.Staff on employment.StaffId equals staff.StaffId
                                                                                join staffPositions in _context.StaffPositions on staff.StaffPositionId equals staffPositions.StaffPositionId
                                                                                select new DashboardStructs.StaffDashboardDisplay
                                                                                {
                                                                                    StaffId = staff.StaffId,
                                                                                    StaffName = staff.StaffName,
                                                                                    StaffContactNum = staff.StaffContactNum,
                                                                                    StaffPosition = staffPositions.StaffPositionName
                                                                                }).Take(5).ToListAsync();


                ViewBag.Bookings = bookingsList;
                ViewBag.Staff = staffList;

                return View(venueToDisplay);
            }
            else
            {
                return StatusCode(401);
            }
        }

        /* DATABASE LINKED CODE */

        private async Task<int> CallAddVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int adminId)
        {
            //TODO: Replace this once retrieval of admin ID is possible
            adminId = 1;
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



        /*   GENERATED CODE   */

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venues = await _context.Venues.FindAsync(id);
            if (venues == null)
            {
                return NotFound();
            }
            return View(venues);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,VenuePostcode,AddLineOne,AddLineTwo,City,County")] Venues venues)
        {
            if (id != venues.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenuesExists(venues.VenueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venues);
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

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venues = await _context.Venues.FindAsync(id);
            _context.Venues.Remove(venues);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(string venueName, string venuePostcode, string addLineOne, string addLineTwo, string city, string county, int adminId)
        {
            int venueId = await CallAddVenueSP(venueName, addLineOne, addLineTwo, city, county, venuePostcode, adminId);
            return RedirectToAction(nameof(Dashboard), new { id = venueId });
        }

        public async Task<IActionResult> Dashboard(int? id)
        {
            _context.Interceptor.SetAdminId(1);

            Venues venueToDisplay = await _context.Venues.FindAsync(id);


            if (venueToDisplay != null)
            {

                // Grab necessary data for bookings from database
                List<DashboardStructs.BookingDashboardDisplay> bookingsList = await (from venue in _context.Venues
                                          join booking in _context.Bookings on venue.VenueId equals booking.VenueId
                                          join bookingAttendees in _context.BookingAttendees on booking.BookingId equals bookingAttendees.BookingId
                                          join customers in _context.Customers on bookingAttendees.CustomerId equals customers.CustomerId
                                          select new DashboardStructs.BookingDashboardDisplay{ 
                                              BookingId = booking.BookingId,
                                              BookingTime = booking.BookingTime,
                                              BookingSize = booking.BookingSize,
                                              BookingAttended = bookingAttendees.BookingAttended,
                                              BookingCustomerName = customers.CustomerName
                                          }).Take(5).ToListAsync();

                // Grab necessary data for staff from database
                List<DashboardStructs.StaffDashboardDisplay> staffList = await (from venue in _context.Venues
                                       join employment in _context.Employment on venue.VenueId equals employment.VenueId
                                       join staff in _context.Staff on employment.StaffId equals staff.StaffId
                                       select new DashboardStructs.StaffDashboardDisplay { 
                                           StaffId = staff.StaffId,
                                           StaffName = staff.StaffName,
                                           StaffContactNum = staff.StaffContactNum,
                                           StaffPosition = staff.StaffPosition
                                       }).Take(5).ToListAsync();


                ViewBag.Bookings = bookingsList;
                ViewBag.Staff = staffList;

                return View(venueToDisplay);
            }
            else
            {
                return StatusCode(401);
            }
        }

        private async Task<int> CallAddVenueSP(string venueName, string venueAddressLineOne, string venueAddressLineTwo, string venueCity, string venueCounty, string venuePostcode, int adminId)
        {
            //TODO: Replace this once retrieval of admin ID is possible
            adminId = 1;
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

        private bool VenuesExists(int id)
        {
            return _context.Venues.Any(e => e.VenueId == id);
        }
    }
}
