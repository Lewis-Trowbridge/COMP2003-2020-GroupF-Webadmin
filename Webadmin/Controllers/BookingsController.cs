using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;
using Microsoft.Extensions.Configuration;

namespace Webadmin.Controllers
{
    public class BookingsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public BookingsController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(int venueId)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, venueId, _context) || WebadminHelper.StaffPermissionVenue(HttpContext.Session, venueId, _context))
            {
                ViewBag.venueId = venueId;
                return View(await _context.Bookings
                .Where(booking => booking.VenueId.Equals(venueId))
                .Where(booking => booking.BookingAttendees.Any(attendees => attendees.BookingAttended.Equals(false)))
                .OrderBy(booking => booking.BookingTime)
                .ToListAsync());
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Attended (int bookingId, int venueId)
        {
            int staffId = WebadminHelper.GetStaffId(HttpContext.Session).Value;
            CallAttended(bookingId, staffId);
            return RedirectToAction(nameof(Index), new { venueId = venueId });
        }

        private void CallAttended(int bookingId, int staffId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@booking_id", bookingId);
            parameters[1] = new SqlParameter("@staff_id", staffId);
            _context.Database.ExecuteSqlRaw("EXEC attended_bookings @booking_id, @staff_id", parameters);
        }

        [HttpPost]
        public IActionResult Cancel (int bookingId, int venueId)
        {
            CallCancelSP(bookingId);
            return RedirectToAction(nameof(Index), new { venueId = venueId});
        }

        private void CallCancelSP (int bookingId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@booking_id", bookingId);
            _context.Database.ExecuteSqlRaw("EXEC cancel_bookings @booking_id", parameters);
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}