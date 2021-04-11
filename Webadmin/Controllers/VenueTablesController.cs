using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;

namespace Webadmin.Controllers
{
    public class VenueTablesController : Controller
    {
        private readonly COMP2003_FContext _context;

        public VenueTablesController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: VenueTables
        public async Task<IActionResult> Index()
        {
            var cleanTableDbContext = _context.VenueTables.Include(v => v.Venue);
            return View(await cleanTableDbContext.ToListAsync());
        }

        // GET: VenueTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueTableId == id);
            if (venueTables == null)
            {
                return NotFound();
            }

            return View(venueTables);
        }

        // GET: VenueTables/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "AddLineOne");
            return View();
        }

        // GET: VenueTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables.FindAsync(id);
            if (venueTables == null)
            {
                return NotFound();
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "AddLineOne", venueTables.VenueId);
            return View(venueTables);
        }

        // GET: VenueTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueTableId == id);
            if (venueTables == null)
            {
                return NotFound();
            }

            return View(venueTables);
        }

        // GET: VenueTables/Create
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "AddLineOne");
            return View();
        }

        // GET: VenueTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables.FindAsync(id);
            if (venueTables == null)
            {
                return NotFound();
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "AddLineOne", venueTables.VenueId);
            return View(venueTables);
        }

        // GET: VenueTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables
                .Include(v => v.Venue)
                .FirstOrDefaultAsync(m => m.VenueTableId == id);
            if (venueTables == null)
            {
                return NotFound();
            }

            return View(venueTables);
        }

        private bool VenueTablesExists(int id)
        {
            return _context.VenueTables.Any(e => e.VenueTableId == id);
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int venueTableId)
        {
            CallDeleteTableSP(venueTableId);
            return RedirectToAction(nameof(Index));
        }

        private void CallDeleteTableSP(int venueTableId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableId);
            _context.Database.ExecuteSqlRaw("EXEC delete_venue_table @venue_table_id", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int venuteTableID, int venuteTableNumber, int numberOfSeats)
        {
            CallCreateTableSP(venueId, venueTableNum, venueTableCapacity);
            return RedirectToAction(nameof(Index));
        }

        private void CallCreateTableSP(int venueId, int venueTableNum, int venueTableCapacity)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_id", venueId);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNum);
            parameters[2] = new SqlParameter("@venue_table_capacity", venueTableCapacity);
            _context.Database.ExecuteSqlRaw("EXEC add_venue_table @venue_id, @venue_table_number, @venue_table_capacity", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int venueTableId, int venueId, int venueTableNum, int venueTableCapacity)
        {
            CallEditTableSP(venueTableId, venueId, venueTableNum, venueTableCapacity);
            return RedirectToAction(nameof(Index));
        }

        private void CallEditTableSP(int venueTableId, int venueId, int venueTableNum, int venueTableCapacity)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableId);
            parameters[1] = new SqlParameter("@venue_id", venueId);
            parameters[2] = new SqlParameter("@venue_table_number", venueTableNum);
            parameters[3] = new SqlParameter("@venue_table_capacity", venueTableCapacity);
            _context.Database.ExecuteSqlRaw("EXEC update_venue_table @venue_table_id, @venue_id, @venue_table_number, @venue_table_capacity", parameters);
        }
    }
}
