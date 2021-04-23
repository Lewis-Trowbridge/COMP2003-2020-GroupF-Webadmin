using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webadmin.Models;
using Webadmin.Controllers;
using Webadmin.Tests.Helpers;
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
            Admins testAdmin = AdminsControllerTestHelper.GetTestAdmin(0);

            // Act
            var actionResult = await controller.Create(testAdmin.AdminUsername, testAdmin.AdminPassword);
            Admins realAdmin = await dbContext.Admins
                .Where(admin => admin.AdminUsername.Equals(testAdmin.AdminUsername))
                .SingleAsync();

            // Assert
            Assert.Equal(testAdmin.AdminUsername, realAdmin.AdminUsername);
            // Test that password has been correctly hashed
            Assert.True(BCrypt.Net.BCrypt.Verify(testAdmin.AdminPassword, realAdmin.AdminPassword));
            

        }
    }
}
