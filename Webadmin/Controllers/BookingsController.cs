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
                return View(await _context.Bookings
                .Where(booking => booking.VenueId.Equals(venueId))
                .ToListAsync());
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Attended (int bookingId)
        {
            int staffId = WebadminHelper.GetStaffId(HttpContext.Session).Value;
            CallAttended(bookingId, staffId);
            return RedirectToAction(nameof(Index));
        }

        private void CallAttended(int bookingId, int staffId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@booking_id", bookingId);
            parameters[1] = new SqlParameter("@staff_id", staffId);
            _context.Database.ExecuteSqlRaw("EXEC attended_bookings @booking_id, @staff_id", parameters);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,BookingTime,BookingSize")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingTime,BookingSize")] Bookings bookings)
        {
            if (id != bookings.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingId))
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
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int bookingSize, int customerId, int venueId, int venueTableId, DateTime bookingTime)
        {
            CallEditSP(bookingSize, customerId, venueId, venueTableId, bookingTime);
            return RedirectToAction(nameof(Index));
        }

        private void CallEditSP (int bookingSize, int customerId, int venueId, int venueTableId, DateTime bookingTime)
        {
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@booking_size", bookingSize);
            parameters[1] = new SqlParameter("@customer_id", customerId);
            parameters[2] = new SqlParameter("@venue_id", venueId);
            parameters[3] = new SqlParameter("@venue_table_id", venueTableId);
            parameters[4] = new SqlParameter("@booking_time", bookingTime);
            _context.Database.ExecuteSqlRaw("EXEC edit_bookings @booking_size, @customer_id, @venue_id, @venue_table_id, @booking_time", parameters);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit ()

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(bookings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Cancel (int bookingId)
        {
            CallCancelSP(bookingId);
            return RedirectToAction(nameof(Index));
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