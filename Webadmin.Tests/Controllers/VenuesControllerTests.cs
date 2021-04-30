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
    public class VenuesControllerTests
    {
        private COMP2003_FContext dbContext;
        private VenuesController controller;

        public VenuesControllerTests()
        {
            dbContext = WebadminTestHelper.GetDbContext();
            controller = new VenuesController(dbContext);
            // Set HttpContext to HttpContext substitute
            controller.ControllerContext.HttpContext = new HttpContextSub();
        }

        [Fact]
        public async void Create_WithValidInputs_AddsSuccessfully()
        {
            // Arrange
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            Admins testAdmin = WebadminTestHelper.GetTestAdmin(0);
            await dbContext.AddAsync(testAdmin);
            await dbContext.SaveChangesAsync();
            controller.ControllerContext.HttpContext.Session.SetInt32(WebadminHelper.AdminIdKey, testAdmin.AdminId);

            Venues testVenue = WebadminTestHelper.GetTestVenue(0);
            var testRequest = VenuesControllerTestHelper.GetCreateVenueRequest();

            // Act
            var actionResult = await controller.Create(testRequest);
            var redirectToActionResult = actionResult as RedirectToActionResult;

            // Assert
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.VenueName.Equals(testRequest.VenueName)));
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.VenuePostcode.Equals(testRequest.VenuePostcode)));
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.City.Equals(testRequest.VenueCity)));
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.County.Equals(testRequest.VenueCounty)));
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.AddLineOne.Equals(testRequest.VenueAddLineOne)));
            Assert.True(await dbContext.Venues.AnyAsync(venue => venue.AddLineTwo.Equals(testRequest.VenueAddLineTwo)));


        }

        [Fact]
        public async void Edit_WithValidInputs_EditsSuccessfully()
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

            var testRequest = VenuesControllerTestHelper.GetEditVenueRequest(testVenue);

            // Act
            var actionResult = await controller.Edit(testRequest);

            // Assert
            Assert.IsType<RedirectToActionResult>(actionResult);

        }

        [Fact]
        public async void Delete_WithValidInputs_DeletesSuccessfully()
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

            var testRequest = VenuesControllerTestHelper.GetDeleteVenueRequest(testVenue);

            // Act
            var actionResult = await controller.Delete(testRequest);

            // Assert
            Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.DoesNotContain(testVenue, dbContext.Venues);
        }

    }
}
