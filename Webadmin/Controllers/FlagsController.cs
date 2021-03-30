using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;
using Microsoft.Data.SqlClient;

namespace Webadmin.Controllers
{
    public class FlagsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public FlagsController(COMP2003_FContext context)
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            //either add stored procedure here or create new subroutine and view for it -- venuesController does with new subroutine and view
            if (ModelState.IsValid)
            {
                _context.Add(flags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flags);
        }

        //-- create new flag --
        public async Task<IActionResult> CreateFlag()
        {
            return View("FlagForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFlag(string flagTitle, string flagLocationPage, string flagCategory, 
            bool flagPersistent, int flagUrgency, string flagDesc, int flagVenueId, DateTime flagDate, bool flagResolved)
        {
            ViewResult result = await CallAddFlagSP(flagTitle, flagLocationPage, flagCategory, flagPersistent, flagUrgency, flagDesc, flagVenueId, flagDate, flagResolved);
            
            //TODO: Replace this with returning of the dashboard view once implemented
            return result;
        }
        private async Task<ViewResult> CallAddFlagSP(string flagTitle, string flagLocationPage, string flagCategory,
            bool flagPersistent, int flagUrgency, string flagDesc, int flagVenueId, DateTime flagDate, bool flagResolved)
        {
            // Initialisation of parameters - long and monotonous but necessary
            SqlParameter[] parameters = new SqlParameter[9];

            //parameters[0] = new SqlParameter("@id", 1); //done through identity server side, might take out

            parameters[0] = new SqlParameter("@flag_title", flagTitle);
            parameters[1] = new SqlParameter("@flag_location_page", flagLocationPage);
            parameters[2] = new SqlParameter("@flag_category", flagCategory);
            parameters[3] = new SqlParameter("@flag_persistent", flagPersistent);
            parameters[4] = new SqlParameter("@flag_urgency", flagUrgency);
            parameters[5] = new SqlParameter("@flag_desc", flagDesc);
            parameters[6] = new SqlParameter("@flag_venue_id", flagVenueId);
            parameters[7] = new SqlParameter("@flag_date", flagDate);
            parameters[8] = new SqlParameter("@flag_resolved", flagResolved);          



            // Executes the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC add_error @flag_title, @flag_location_page, @flag_category, @flag_persistent, @flag_urgency, @flag_desc, @flag_venue_id, @flag_date, @flag_resolved", parameters);

            //return the id of the flag just created - set to 1 for now but change **
            return View("FlagForm");
        }
        //-- /create new flag --

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
            if (id != flags.Id)
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
                    if (!FlagsExists(flags.Id))
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
            return _context.Flags.Any(e => e.Id == id);
        }
    }
}
