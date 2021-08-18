using CustomerAPI.Controllers;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace CustomerAPITestProject
{
    public class CustomersControllerTest
    {
        #region Private Variable Declaration
        CustomersController _controller;
        #endregion

        #region Constructor
        public CustomersControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseInMemoryDatabase("CustomerDB");
            var context = new CustomerContext(optionsBuilder.Options);

            _controller = new CustomersController(context);
        }
        #endregion

        #region Test Methods
        [Fact]
        public async void GetCustomers()
        {
            var okResult = await _controller.GetCustomers();
            Assert.True(okResult != null);
        }

        [Fact]
        public async void GetCustomer_ReturnsNotFound()
        {
            var okResult = await _controller.GetCustomer(0);
            Assert.IsType<NotFoundResult>(okResult.Result);
        }

        [Fact]
        public async void PutCustome_ReturnsNotBadRequest()
        {
            Customer testObj = new Customer();
            testObj.Id = 2;
            testObj.FirstName = "AAA";

            var okResult = await _controller.PutCustomer(1, testObj);
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public async void PostCustomer()
        {
            Customer testObj = new Customer();
            testObj.FirstName = "AAA";
            testObj.LastName = "BBB";
            testObj.DateOfBirth = new DateTime(1989, 1, 1);
            testObj.BusinessName = "AAA";
            testObj.RecordCreatedOn = DateTime.Now;

            var okResult = await _controller.PostCustomer(testObj);
            Assert.IsType<CreatedAtActionResult>(okResult.Result);
        }

        [Fact]
        public async void DeleteCustomer_ReturnsNotFound()
        {
            var okResult = await _controller.DeleteCustomer(0);
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion
    }
}
