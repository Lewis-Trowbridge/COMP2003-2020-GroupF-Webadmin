using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;

namespace Webadmin.Controllers
{
    public class FlagsController : Controller
    {
        private readonly cleanTableDbContext _context;

        public FlagsController(cleanTableDbContext context)
        {
            _context = context;
        }

        // GET: Flags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flags.ToListAsync());
        }

        // GET: Flags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flags = await _context.Flags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flags == null)
            {
                return NotFound();
            }

            return View(flags);
        }

        // GET: Flags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FlagTitle,FlagLocationPage,FlagCategory,FlagPersistent,FlagUrgency,FlagDesc,FlagVenueID,FlagDate,FlagResolved")] Flags flags)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flags);
        }

        // GET: Flags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flags = await _context.Flags.FindAsync(id);
            if (flags == null)
            {
                return NotFound();
            }
            return View(flags);
        }

        // POST: Flags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FlagTitle,FlagLocationPage,FlagCategory,FlagPersistent,FlagUrgency,FlagDesc,FlagVenueID,FlagDate,FlagResolved")] Flags flags)
        {
            if (id != flags.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flags);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlagsExists(flags.ID))
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
            return View(flags);
        }

        // GET: Flags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flags = await _context.Flags
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flags == null)
            {
                return NotFound();
            }

            return View(flags);
        }

        // POST: Flags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flags = await _context.Flags.FindAsync(id);
            _context.Flags.Remove(flags);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlagsExists(int id)
        {
            return _context.Flags.Any(e => e.ID == id);
        }
    }
}
