﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;
using Webadmin.Requests;
using PhoneNumbers;

namespace Webadmin.Controllers
{
    public class StaffsController : Controller
    {
        private readonly COMP2003_FContext _context;

        public StaffsController(COMP2003_FContext context)
        {
            _context = context;
        }

        // GET: Staffs/Index/id
        public async Task<IActionResult> Index(int venueId)
        {
            ViewBag.venueId = venueId;
            var adminId = WebadminHelper.GetAdminId(HttpContext.Session);
            return View(await _context.Staff
                // Joining the staffId from Emplyment table
                .Join(_context.Employment, staff => staff.StaffId, employment => employment.StaffId, (staff, employment) => new
                {
                    Employment = employment,
                    Staff = staff
                })
                // Filters the staff depending on the veuneId selected
                .Where(staffAndEmployment => staffAndEmployment.Employment.VenueId.Equals(venueId))
                // Joining the AdminLocations to Emplyment using the selected VenueId
                .Join(_context.AdminLocations, venue => venue.Employment.VenueId, location => location.VenueId, (venue, location) => new
                {
                    Staff = venue.Staff,
                    Locations = location
                })
                // Checking Admin has permission to view staff within venue
                .Where(locationAndStaff => locationAndStaff.Locations.AdminId.Equals(adminId))
                // Getting the remaining staff
                .Select(staff => staff.Staff)
                // Load related shifts
                .Include(staff => staff.StaffShifts)
                // Putting it into a list view
                .ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, id.Value, _context) || WebadminHelper.GetStaffId(HttpContext.Session) == id)
            {
                var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
                if (staff == null)
                {
                    return NotFound();
                }

                return View(staff);
            }
            return Unauthorized();
        }

        // Create new staff details

        // GET /Staffs/Create/5
        public IActionResult Create(int id) //venue id
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, id, _context))
            {
                ViewBag.venueId = id;
                return View();
            }
            return Unauthorized();
        }

        // Post: Allows you to add new staff 
        [HttpPost] // /Staffs/Create/id
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStaffRequest request)
        {
            if (WebadminHelper.AdminPermissionVenue(HttpContext.Session, request.VenueId, _context))
            {
                string formattedContactNumber = TryConvertContactNumber(request.StaffContactNum);
                if (formattedContactNumber != null)
                {
                    CallAddStaffSP(request.StaffName, request.StaffContactNum, request.StaffPosition,request.VenueId);
                    return RedirectToAction(nameof(Index), new { venueId = request.VenueId });
                }
                else
                {
                    ModelState.AddModelError("staffContactNum", "Please input a valid UK phone number.");
                    ViewBag.venueId = request.VenueId;
                    return View();
                }
                
            }
            return Unauthorized();
        }

        // ClockIn staff 
        
        public IActionResult ClockIn(int staffId, int venueId)
        {            
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, staffId, _context))
            {
                CallClockInSP(staffId);
                return RedirectToAction(nameof(Index), new { venueId = venueId });

            }
            return Unauthorized();

        }

        // Edit staff details

        // GET /Staffs/
        public async Task<IActionResult> Edit(int staffId, int venueId)
        {
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, staffId, _context))
            {
                ViewBag.staffId = staffId;
                ViewBag.VenueId = venueId;
                var staff = await _context.Staff
                    .Where(staff => staff.StaffId.Equals(staffId))
                    .Select(staff => new EditStaffRequest 
                    { 
                        StaffId = staff.StaffId,
                        StaffName = staff.StaffName,
                        StaffContactNum = staff.StaffContactNum,
                        StaffPosition = staff.StaffPosition,
                        VenueId = venueId
                    })
                    .SingleAsync();
                return View(staff);
            }
            return Unauthorized();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStaffRequest request)
        {
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, request.StaffId, _context))
            {
                string formattedContactNumber = TryConvertContactNumber(request.StaffContactNum);
                if (formattedContactNumber != null)
                {
                    CallEditStaffSP(request.StaffName, request.StaffContactNum, request.StaffPosition, request.StaffId);
                }
                else
                {
                    ViewBag.staffId = request.StaffId;
                    ViewBag.VenueId = request.VenueId;
                    ModelState.AddModelError("staffContactNum", "Please input a valid UK phone number.");
                    return View();
                }
                return RedirectToAction(nameof(Index), new { venueId = request.VenueId });
            }
            return Unauthorized();
        }

        // Delete staff details

        //GET /Staffs/Delete/ID
        public async Task<IActionResult> Delete(int? id, int venueId)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, id.Value, _context))
            {
                ViewBag.VenueId = venueId;
                var staff = await _context.Staff
                .FirstOrDefaultAsync(m => m.StaffId == id);
                if (staff == null)
                {
                    return NotFound();
                }
                return View(staff);
            }
            return Unauthorized();
        }
        [HttpPost] // /Staffs/Delete/ID
        public IActionResult Delete(int staffId, int venueId)
        {
            if (WebadminHelper.AdminPermissionStaff(HttpContext.Session, staffId, _context))
            {
                CallDeleteStaffSP(staffId);
                return RedirectToAction(nameof(Index), new { venueId = venueId });
            }
            return Unauthorized();
        }

        private string TryConvertContactNumber(string contactNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(contactNumber, "GB");
                if (phoneNumberUtil.IsValidNumberForRegion(phoneNumber, "GB"))
                {
                    // If the phone number if a valid UK number, return the formatted string
                    return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
                }
                // If the phone number is not a valid UK number, signal this with a null
                else
                {
                    return null;
                }
            }
            // If there are any general issues with the formatting of the phone number, signal this with a null
            catch (NumberParseException)
            {
                return null;
            }

        }

        /*  DATABASE LINKED CODE  */

        // Execute add_staff stored procedure on SQL Database
        private void CallAddStaffSP(string staffName, string staffContactNum, string staffPosition, int venueId)
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


        private void CallClockInSP(int staffId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@staff_id", staffId);
            _context.Database.ExecuteSqlRaw("EXEC clock_in_staff @staff_id", parameters);
        }
    }
}