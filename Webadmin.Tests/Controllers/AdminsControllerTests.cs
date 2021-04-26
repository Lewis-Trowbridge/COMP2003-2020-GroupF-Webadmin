using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Webadmin.Models;
using Webadmin.Controllers;
using Webadmin.Tests.Helpers;
using Webadmin.Requests;
using BCrypt;
using HttpContextSubs;
using Xunit;

namespace Webadmin.Tests.Controllers
{
    public class AdminsControllerTests
    {
        private COMP2003_FContext dbContext;
        private AdminsController controller;

        public AdminsControllerTests()
        {
            dbContext = WebadminTestHelper.GetDbContext();
            controller = new AdminsController(dbContext);
            // Set HttpContext to HttpContext substitute
            controller.ControllerContext.HttpContext = new HttpContextSub();
        }

        [Fact]
        public async void Create_WithValidInputs_AddsSuccessfully()
        {
            // Arrange
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            Admins testAdmin = WebadminTestHelper.GetTestAdmin(0);
            CreateAdminRequest testRequest = AdminsControllerTestHelper.GetCreateAdminRequest(testAdmin);

            // Act
            var actionResult = await controller.Create(testRequest);
            Admins realAdmin = await dbContext.Admins
                .Where(admin => admin.AdminUsername.Equals(testAdmin.AdminUsername))
                .SingleAsync();

            // Assert
            Assert.IsType<CreatedAtActionResult>(actionResult);
            Assert.Equal(testAdmin.AdminUsername, realAdmin.AdminUsername);
            // Test that password has been correctly hashed
            Assert.True(BCrypt.Net.BCrypt.Verify(testAdmin.AdminPassword, realAdmin.AdminPassword));


        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("uj34s1V7DrUF0gjmKQh0yjysx89LEHPgBOWTD5cuRmPtLGJS1q4thSWZgoNsNJQuIFrCPjCrOdYkcHYJ", "no length limit because of hashing")]
        public async void Create_WithInvalidInputs_Fails(string username, string password)
        {
            // Arrange
            Admins testAdmin = new Admins
            {
                AdminUsername = username,
                AdminPassword = password
            };
            CreateAdminRequest testRequest = AdminsControllerTestHelper.GetCreateAdminRequest(testAdmin);
            controller.ModelState.AddModelError("invalid parameters", "Invalid parameters supplied.");

            // Act
            var actionResult = await controller.Create(testRequest);


            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
            Assert.False(await dbContext.Admins.AnyAsync(admin => admin.AdminUsername.Equals(username)));
        }

    }
}
