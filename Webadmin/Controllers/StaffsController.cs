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

        // Post: To add a staff member to a venue
        [HttpPost]
        [ValidateAntiForgeryToken]
        private async void CallAddSaffSP(string StaffName, int StaffContactNum, int StaffPositionId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@staff_name", StaffName);
            parameters[1] = new SqlParameter("@staff_contact_num", StaffContactNum);
            parameters[2] = new SqlParameter("@staff_position_id", StaffPositionId);

            await _context.Database.ExecuteSqlRawAsync("EXEC add_staff, @staff_name, @staff_contact_num, @staff_position_id", parameters);
        }
    }
}
