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

        private bool VenueTablesExists(int id)
        {
            return _context.VenueTables.Any(e => e.VenueTableId == id);
        }

        public async Task<IActionResult> Delete (int venueTableID)
        {
            CallDeleteTableSP(venueTableID);
            return RedirectToAction(nameof(Index));
        }

        private void CallDeleteTableSP(int venueTableID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableID);
            _context.Database.ExecuteSqlRaw("EXEC delete_venue_tables @venue_table_id", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Create (int venueID, int venueTableNumber, int numberOfSeats)
        {
            CallCreateTableSP(venueID, venueTableNumber, numberOfSeats);
            return RedirectToAction(nameof(Index));
        }

        private void CallCreateTableSP(int venueID, int venueTableNumber, int numberOfSeats)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_id", venueID);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNumber);
            parameters[2] = new SqlParameter("@venue_table_capacity", numberOfSeats);
            _context.Database.ExecuteSqlRaw("EXEC add_venue_tables @venue_id, @venue_table_number, @venue_table_capacity", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int venueTableID, int venueTableNumber, int numberOfSeats)
        {
            CallEditTableSP(venueTableID, venueTableNumber, numberOfSeats);
            return RedirectToAction(nameof(Index));
        }

        private void CallEditTableSP(int venueTableID, int venueTableNumber, int numberOfSeats)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableID);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNumber);
            parameters[2] = new SqlParameter("@venue_table_capacity", numberOfSeats);
            _context.Database.ExecuteSqlRaw("EXEC edit_venue_tables @venue_table_id, @venue_table_number, @venue_table_capacity", parameters);
        }
    }
}
