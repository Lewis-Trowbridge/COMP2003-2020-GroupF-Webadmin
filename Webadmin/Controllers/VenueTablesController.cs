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
        public async Task<IActionResult> Index(int venueId)
        {
            ViewBag.venueId = venueId;
            return View(await _context.VenueTables
                .Where(venueTable => venueTable.VenueId.Equals(venueId))
                .OrderBy(venueTable => venueTable.VenueTableNum)
                .ToListAsync());
        }

        // GET: VenueTables/Details/5
        public async Task<IActionResult> Details(int? venueTableId, int venueId)
        {
            ViewBag.venueId = venueId;
            if (venueTableId == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables
                .FirstOrDefaultAsync(m => m.VenueTableId == venueTableId);
            if (venueTables == null)
            {
                return NotFound();
            }

            return View(venueTables);
        }

        // GET: VenueTables/Create
        public IActionResult Create(int venueId)
        {
            ViewBag.venueId = venueId;
            return View();
        }

        // GET: VenueTables/Edit/5
        public async Task<IActionResult> Edit(int? venueTableId, int venueId)
        {
            ViewBag.venueId = venueId;
            if (venueTableId == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables.FindAsync(venueTableId);
            if (venueTables == null)
            {
                return NotFound();
            }
            return View(venueTables);
        }

        // GET: VenueTables/Delete/5
        public async Task<IActionResult> Delete(int? venueTableId, int venueId)
        {
            ViewBag.venueId = venueId;
            if (venueTableId == null)
            {
                return NotFound();
            }

            var venueTables = await _context.VenueTables
                .FirstOrDefaultAsync(m => m.VenueTableId == venueTableId);
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
        public async Task<IActionResult> Delete(int venueTableId, int venueId)
        {
            await CallDeleteTableSP(venueTableId);
            return RedirectToAction(nameof(Index), new { venueId = venueId });
        }

        private async Task CallDeleteTableSP(int venueTableId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableId);
            await _context.Database.ExecuteSqlRawAsync("EXEC delete_venue_table @venue_table_id", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int venueId, int venueTableNum, int venueTableCapacity)
        {
            await CallCreateTableSP(venueId, venueTableNum, venueTableCapacity);
            return RedirectToAction(nameof(Index), new { venueId = venueId });
        }

        private async Task CallCreateTableSP(int venueId, int venueTableNum, int venueTableCapacity)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_id", venueId);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNum);
            parameters[2] = new SqlParameter("@venue_table_capacity", venueTableCapacity);
            await _context.Database.ExecuteSqlRawAsync("EXEC add_venue_table @venue_id, @venue_table_number, @venue_table_capacity", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int venueTableId, int venueTableNum, int venueTableCapacity, int venueId)
        {
            await CallEditTableSP(venueTableId, venueTableNum, venueTableCapacity);
            return RedirectToAction(nameof(Index), new { venueId = venueId });
        }

        private async Task CallEditTableSP(int venueTableID, int venueTableNum, int venueTableCapacity)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableID);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNum);
            parameters[2] = new SqlParameter("@venue_table_capacity", venueTableCapacity);
            await _context.Database.ExecuteSqlRawAsync("EXEC edit_venue_table @venue_table_id, @venue_table_number, @venue_table_capacity", parameters);
        }
    }
}
