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
        /* GET: To view the page required to add a new member of staff 
         * 
         * GET /Staffs/Create/id
         */
        public IActionResult Create(int id) //venue id
        {
            ViewBag.VenueId = id;
            return View();
        }

        /* Post: Allows you to add new staff */
        [HttpPost] // POST  /Staffs/Create/id
        [ValidateAntiForgeryToken]
        public IActionResult Create(string StaffName, string StaffContactNum, string StaffPosition, int VenueId)
        {
            CallAddSaffSP(StaffName, StaffContactNum, StaffPosition, VenueId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.StaffId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int StaffId, string StaffName, string StaffContactNum, string StaffPosition)
        {
            CallEditStaffSP(StaffId, StaffName, StaffContactNum, StaffPosition);
            return RedirectToAction(nameof(Index));
        }

        /*  DATABASE LINKED CODE  */

        /* Makes a link to the stored procedure */
        private void CallAddSaffSP(string StaffName, string StaffContactNum, string StaffPosition, int VenueId)
        {
            // Ask about a venue ID
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@staff_name", StaffName);
            parameters[1] = new SqlParameter("@staff_contact_num", StaffContactNum);
            parameters[2] = new SqlParameter("@staff_position", StaffPosition);
            parameters[3] = new SqlParameter("@venue_id", VenueId);
            /* Executes 'add_staff' stored procedure*/
            _context.Database.ExecuteSqlRaw("EXEC add_staff @staff_name, @staff_contact_num, @staff_position, @venue_id", parameters);
        }

        private void CallEditStaffSP(int StaffID, string StaffName, string StaffContactNum, string StaffPostion)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@staff_id", StaffID);
            parameters[1] = new SqlParameter("staff_name", StaffName);
            parameters[2] = new SqlParameter("staff_contact_num", StaffContactNum);
            parameters[3] = new SqlParameter("staff_position", StaffPostion);
            _context.Database.ExecuteSqlRaw("EXEC edit_staff @staff_id, @staff_name, @staff_contact_num, @staff_position", parameters);
        }
    }
}
