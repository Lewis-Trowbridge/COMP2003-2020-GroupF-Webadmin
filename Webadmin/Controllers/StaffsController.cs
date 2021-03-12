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

        // GET: Staffs/Indext/id
        public async Task<IActionResult> Index()
        {
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

        // Create new staff details
        
        // GET /Staffs/Create/5
        public IActionResult Create(int id) //venue id
        {
            ViewBag.venueId = id;
            return View();
        }

        // Post: Allows you to add new staff 
        [HttpPost] // /Staffs/Create/id
        [ValidateAntiForgeryToken]
        public IActionResult Create(string staffName, string staffContactNum, string staffPosition, int venueId)
        {
            CallAddSaffSP(staffName, staffContactNum, staffPosition, venueId);
            return RedirectToAction(nameof(Index));
        }

        // Edit staff details

        // GET /Staffs/
        public IActionResult Edit(int id)
        {
            ViewBag.staffId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string staffName, string staffContactNum, string staffPosition, int staffId)
        {
            CallEditStaffSP(staffName, staffContactNum, staffPosition, staffId);
            return RedirectToAction(nameof(Index));
        }

        // Delete staff details

        //GET /Staffs/Delete/ID
        public async Task<IActionResult> Delete(int? id)
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
        [HttpPost] // /Staffs/Delete/ID
        public IActionResult Delete(int staffId)
        {
            CallDeleteStaffSP(staffId);
            return RedirectToAction(nameof(Index));
        }

        /*  DATABASE LINKED CODE  */

        // Execute add_staff stored procedure on SQL Database
        private void CallAddSaffSP(string staffName, string staffContactNum, string staffPosition, int venueId)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@staff_name", staffName);
            parameters[1] = new SqlParameter("@staff_contact_num", staffContactNum);
            parameters[2] = new SqlParameter("@staff_position", staffPosition);
            parameters[3] = new SqlParameter("@venue_id", venueId);
            _context.Database.ExecuteSqlRaw("EXEC add_staff @staff_name, @staff_contact_num, @staff_position, @venue_id", parameters);
        }

        // Execute edit_staff stored procedure on SQL Database
        private void CallEditStaffSP(string staffName, string staffContactNum, string staffPosition, int staffId)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@staff_name", staffName);
            parameters[1] = new SqlParameter("@staff_contact_num", staffContactNum);
            parameters[2] = new SqlParameter("@staff_position", staffPosition);
            parameters[3] = new SqlParameter("@staff_id", staffId);
            _context.Database.ExecuteSqlRaw("EXEC edit_staff @staff_id, @staff_name, @staff_contact_num, @staff_position", parameters);
        }

        // Execute delete_staff stored procedure on SQL Database
        private void CallDeleteStaffSP(int staffId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@staff_id", staffId);
            _context.Database.ExecuteSqlRaw("EXEC delete_staff @staff_id", parameters);
        }
    }
}
