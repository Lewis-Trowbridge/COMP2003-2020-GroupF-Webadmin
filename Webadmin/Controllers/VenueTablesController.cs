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
        private readonly cleanTableDbContext _context;

        public VenueTablesController(cleanTableDbContext context)
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

        [HttpPost]
        public Task<IActionResult> Table_update(int venueID, int newTable)
        {
            _context.Database.ExecuteSqlRaw("EXEC edit_tables @venue_id, @new_tables",
                new SqlParameter("@venue_id", venueID),
                new SqlParameter("@new_tables", newTable));
            return null;
        }
    }
}
