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
    public class StaffsController : Controller
    {
        private readonly cleanTableDbContext _context;

        public StaffsController(cleanTableDbContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {;
            return View(await _context.Staff.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }
        /* GET: To view the page required to add a new member of staff */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /* Post: Allows you to add new staff */
        [HttpPost("{venueId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int venueId, string StaffName, int StaffContactNum)
        {
            // TODO: Look into view bags and getting the venueId so the staff will be put into correct id. It will look something like this
            ViewBag.VenueId = venueId;
            // *****
            CallAddSaffSP(StaffName, StaffContactNum);
            return RedirectToAction();
        }

        // Some stuff to read on getting the venue ID
        // https://www.tutorialsteacher.com/mvc/viewbag-in-asp.net-mvc
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#http-verb-templates


        


        /*  DATABASE LINKED CODE  */

        /* Makes a link to the stored procedure */
        private async void CallAddSaffSP(string StaffName, int StaffContactNum)
        {
            // Ask about a venue ID
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@staff_name", StaffName);
            parameters[1] = new SqlParameter("@staff_contact_num", StaffContactNum);
            /* Executes 'add_staff' stored procedure*/
            await _context.Database.ExecuteSqlRawAsync("EXEC add_staff, @staff_name, @staff_contact_num, @staff_position_id", parameters);
        }




        /*   GENERATED CODE   */

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["StaffPositionId"] = new SelectList(_context.StaffPositions, "StaffPositionId", "StaffPositionId", staff.StaffPositionId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,StaffName,StaffContactNum,StaffPositionId")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
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
            ViewData["StaffPositionId"] = new SelectList(_context.StaffPositions, "StaffPositionId", "StaffPositionId", staff.StaffPositionId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.StaffPosition)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.StaffId == id);
        }

    }
}
