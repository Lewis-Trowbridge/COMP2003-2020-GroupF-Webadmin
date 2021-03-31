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
        public IActionResult Login(int adminId)
        {
            HttpContext.Session.SetInt32(Webadminhelper.AdminIdKey, adminId);
            return RedirectToAction("Index", "Venues");
        }
    }
}
