using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webadmin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAdmin(int adminId)
        {
            HttpContext.Session.SetInt32(WebadminHelper.AdminIdKey, adminId);
            return RedirectToAction("Index", "Venues");
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
