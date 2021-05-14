using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webadmin.Controllers;
using Webadmin.Models;
using Webadmin.Requests;
using BCrypt.Net;

namespace Webadmin.Controllers
{
    public class AdminsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public AdminsController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Hash password using BCrypt using OWASP's recommended work factor of 12

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.AdminPassword, workFactor: 12);

            string response = await CallCreateAdminSP(request.AdminUsername, hashedPassword);
            if (response.Substring(0, 3) == "200")
            {
                // Get new ID
                int newId = Convert.ToInt32(response.Substring(3));
                // Log new user in
                HttpContext.Session.SetInt32(WebadminHelper.AdminIdKey, newId);
                // Redirect to details page
                return RedirectToAction(nameof(Details), new { Id = newId });
            }
            else
            {
                return View();
            }
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.adminId = id;
            var admins = await _context.Admins
                .Where(admin => admin.AdminId.Equals(id))
                .Select(admin => new EditAdminRequest
                {
                    AdminUsername = admin.AdminUsername
                }
                )
                .SingleAsync();
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAdminRequest admin)
        {
            if (admin.AdminPassword != null)
            {
                // Hash password using BCrypt using OWASP's recommended work factor of 12
                admin.AdminPassword = BCrypt.Net.BCrypt.HashPassword(admin.AdminPassword, workFactor: 12);
            }

            string response = await CallEditAdminSP(id, admin.AdminUsername, admin.AdminPassword);
            switch (response.Substring(0, 3))
            {
                case "200":
                    return RedirectToAction(nameof(Details), new { Id = id });
                case "404":
                    return NotFound();
                default:
                    return StatusCode(500);
            }
            
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.adminId = id;
            var admins = await _context.Admins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // POST: Admins/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int adminid)
        {
            await CallDeleteAdminSP(adminid);
            return RedirectToAction("Index", "Home");
        }

        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }

        private async Task<string> CallCreateAdminSP(string adminUsername, string adminPassword)
        {
            SqlParameter[] parameters = new SqlParameter[3];

            parameters[0] = new SqlParameter("@admin_username", adminUsername);
            parameters[1] = new SqlParameter("@admin_password", adminPassword);
            parameters[2] = new SqlParameter
            {
                ParameterName = "@response",
                Direction = System.Data.ParameterDirection.Output,
                Size = 100
            };

            // Executes the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC add_admin @admin_username, @admin_password, @response OUTPUT", parameters);

            return (string)parameters[2].Value;
        }

        private async Task<string> CallEditAdminSP(int adminId, string adminUsername, string adminPassword)
        {
            SqlParameter[] parameters = new SqlParameter[4];

            parameters[0] = new SqlParameter("@admin_id", adminId);
            parameters[1] = CheckIfNull("@admin_username", adminUsername);
            parameters[2] = CheckIfNull("@admin_password", adminPassword);
            parameters[3] = new SqlParameter
            {
                ParameterName = "@response",
                Direction = System.Data.ParameterDirection.Output,
                Size = 100
            };

            // Executes the stored procedure
            await _context.Database.ExecuteSqlRawAsync("EXEC edit_admin @admin_id, @admin_username, @admin_password, @response OUTPUT", parameters);

            return (string)parameters[3].Value;
        }

        private async Task CallDeleteAdminSP(int adminId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@admin_id", adminId);
            await _context.Database.ExecuteSqlRawAsync("EXEC delete_admin @admin_id", parameters);
        }

        private SqlParameter CheckIfNull(string parameterName, string stringToCheck)
        {
            if (stringToCheck != null)
            {
                return new SqlParameter(parameterName, stringToCheck);
            }
            else
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }
        }
    }
}
