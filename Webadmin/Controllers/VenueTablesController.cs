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

        // POST: VenueTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueTableId,VenueId,VenueTableNum,VenueTableCapacity")] VenueTables venueTables)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venueTables);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "AddLineOne", venueTables.VenueId);
            return View(venueTables);
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

        // POST: VenueTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueTableId,VenueId,VenueTableNum,VenueTableCapacity")] VenueTables venueTables)
        {
            if (id != venueTables.VenueTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueTables);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueTablesExists(venueTables.VenueTableId))
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

        // POST: VenueTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueTables = await _context.VenueTables.FindAsync(id);
            _context.VenueTables.Remove(venueTables);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueTablesExists(int id)
        {
            return _context.VenueTables.Any(e => e.VenueTableId == id);
        }

        public async Task<IActionResult> Delete (int venuteTableID)
        {
            CallDeleteTableSP(venuteTableID);
            return RedirectToAction(nameof(Index));
        }

        private void CallDeleteTableSP(int venueTableID)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@venue_table_id", venueTableID);
            _context.Database.ExecuteSqlRaw("EXEC delete_venue_tables @venue_table_id", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Add (int venueID, int venuteTableNumber, int numberOfSeats)
        {
            CallAddTableSP(venueID, venuteTableNumber, numberOfSeats);
            return RedirectToAction(nameof(Index));
        }

        private void CallAddTableSP(int venueID, int venueTableNumber, int numberOfSeats)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@venue_id", venueID);
            parameters[1] = new SqlParameter("@venue_table_number", venueTableNumber);
            parameters[2] = new SqlParameter("@venue_table_capacity", numberOfSeats);
            _context.Database.ExecuteSqlRaw("EXEC add_venue_tables @venue_id, @venue_table_number, @venue_table_capacity", parameters);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int venuteTableID, int venuteTableNumber, int numberOfSeats)
        {
            CallEditTableSP(venuteTableID,venuteTableNumber, numberOfSeats);
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
