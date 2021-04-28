using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webadmin.Models;
using Webadmin.Requests;
using BCrypt.Net;

namespace Webadmin.Controllers
{
    public class LoginController : Controller
    {
        private readonly COMP2003_FContext _context;

        public LoginController(COMP2003_FContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginRequest request)
        {
            try
            {
                Admins storedAdmin = await _context.Admins
                .Where(admin => admin.AdminUsername.Equals(request.Username))
                .SingleAsync();
                if (BCrypt.Net.BCrypt.Verify(request.Password, storedAdmin.AdminPassword))
                {
                    HttpContext.Session.SetInt32(WebadminHelper.AdminIdKey, storedAdmin.AdminId);
                    return RedirectToAction("Index", "Venues");
                }
                else
                {
                    ModelState.AddModelError("Password", "Password is incorrect.");
                    return View(nameof(Index));
                }
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError("Username", "Account not found.");
                return View(nameof(Index));
            }
        }

        public IActionResult LoginStaff(int staffId)
        {
            HttpContext.Session.SetInt32(WebadminHelper.StaffIdKey, staffId);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(WebadminHelper.AdminIdKey);
            HttpContext.Session.Remove(WebadminHelper.StaffIdKey);
            return RedirectToAction("Index", "Home");
        }
    }
}
