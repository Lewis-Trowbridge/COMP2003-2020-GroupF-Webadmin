using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webadmin.Tests.Helpers;
using Webadmin.Models;
using Webadmin.Controllers;
using Webadmin.Requests;
using Xunit;
using HttpContextSubs;

namespace Webadmin.Tests.Controllers
{
    public class StaffControllerTests
    {

        private COMP2003_FContext dbContext;
        private StaffsController controller;

        public StaffControllerTests()
        {
            dbContext = WebadminTestHelper.GetDbContext();
            controller = new StaffsController(dbContext);
            // Set HttpContext to HttpContext substitute
            controller.ControllerContext.HttpContext = new HttpContextSub();
        }

        [Fact]
        public async void Create_WithValidInputs_CreateSuccessful()
        {
            // Arrange
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            Admins testAdmin = WebadminTestHelper.GetTestAdmin(0);
            Venues testVenue = WebadminTestHelper.GetTestVenue(0);
            AdminLocations testLocation = WebadminTestHelper.GetTestAdminLocation(testVenue, testAdmin);
            await dbContext.AddAsync(testAdmin);
            await dbContext.AddAsync(testVenue);
            await dbContext.AddAsync(testLocation);
            await dbContext.SaveChangesAsync();
            controller.ControllerContext.HttpContext.Session.SetInt32(WebadminHelper.AdminIdKey, testAdmin.AdminId);

            Staff testStaff = StaffControllerTestHelper.GetTestStaff(0);
            CreateStaffRequest testRequest = StaffControllerTestHelper.GetCreateStaffRequest(testStaff, testVenue);

            // Act
            var actionResult = controller.Create(testRequest);

            // Assert
            Assert.IsType<RedirectToActionResult>(actionResult);

            Assert.True(await dbContext.Staff.AnyAsync(staff => staff.StaffName.Equals(testRequest.StaffName)));
            Assert.True(await dbContext.Staff.AnyAsync(staff => staff.StaffContactNum.Equals(testRequest.StaffContactNum)));
            Assert.True(await dbContext.Staff.AnyAsync(staff => staff.StaffPosition.Equals(testRequest.StaffPosition)));

        }
    }
}
