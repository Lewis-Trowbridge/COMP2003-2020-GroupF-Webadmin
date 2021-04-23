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
using BCrypt.Net;

namespace Webadmin.Views
{
    public class AdminsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public AdminsController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
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
        public async Task<IActionResult> Create(string adminUsername, string adminPassword)
        {
            // Hash password using BCrypt using OWASP's recommended work factor of 12

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminPassword, workFactor: 12);

            string response = await CallCreateAdminSP(adminUsername, hashedPassword);
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
            var admins = await _context.Admins.FindAsync(id);
            // Remove password to avoid sending it to the view
            admins.AdminPassword = null;
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
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminUsername,AdminPassword,AdminLevel")] Admins admins)
        {
            if (id != admins.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminsExists(admins.AdminId))
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
            return View(admins);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admins = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admins);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
    }
}
