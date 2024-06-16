using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTest
{
    public class StaffControllerTests
    {
        private StaffController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new StaffController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUsers_ReturnsOkResult_WhenUsersExist()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                RoleId = 1,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = true,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers(null, null, 0, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

        }

        [Test]
        public async Task GetUsers_ReturnsNotFound_WhenNoUsersExist()
        {
            // Act
            var result = await _controller.GetUsers(null, null, 0, null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetActiveUsers_ReturnsOkResult_WhenActiveUsersExist()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                RoleId = 1,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = true,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetActiveUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetActiveUsers_ReturnsNotFound_WhenNoActiveUsersExist()
        {
            // Act
            var result = await _controller.GetActiveUsers();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetOrders_ReturnsOkResult_WhenOrdersExist()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "345/356",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 1000,
                OrderStatus = "Pending",
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrders(0, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetOrders_ReturnsNotFound_WhenNoOrdersExist()
        {
            // Act
            var result = await _controller.GetOrders(1, null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetReviews_ReturnsOkResult_WhenReviewsExist()
        {
            // Arrange
            var review = new Review
            {
                ReviewId = 1,
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Date = DateTime.Now,
                Rating = 4,
                Comment = "Good",
                IsRated = false,
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetReviews(null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetReviews_ReturnsNotFound_WhenNoReviewsExist()
        {
            // Act
            var result = await _controller.GetReviews(null);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task DeleteUser_ReturnsOkResult_WhenUserIsDeleted()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                RoleId = 1,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = true,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var deletedUser = await _context.Users.FindAsync(1);
            Assert.IsFalse(deletedUser.IsActive);
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserIsInactive()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                RoleId = 1,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = false,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task SubmitOrder_ReturnsOk_WhenOrderIsPending()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "345/356",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 1000,
                OrderStatus = "Pending",
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.SubmitOrder(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var updatedOrder = await _context.Orders.FindAsync(1);
            Assert.AreEqual("Submitted", updatedOrder.OrderStatus);
        }

        [Test]
        public async Task SubmitOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.SubmitOrder(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task SubmitOrder_ReturnsBadRequest_WhenOrderIsAlreadySubmitted()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "345/356",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 1000,
                OrderStatus = "Submited",
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.SubmitOrder(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task SubmitOrder_ReturnsBadRequest_WhenProductStockIsInsufficient()
        {
            // Arrange
            var product = new Product
            {
                ProductId = 1,
                ForAgeId = 1,
                CategoryId = 1,
                BrandId = 1,
                Name = "Meiji",
                Description = "Sữa bột",
                Price = 1000,
                Stock = 5,
                IsActive = true,
            };
            var orderDetail = new OrderDetail
            {
                OrderDetailId = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 10000,
            };
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "123/34",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 10000,
                OrderStatus = "Pre-Order",
                OrderDetails = new List<OrderDetail> { orderDetail }
            };
            _context.Products.Add(product);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.SubmitOrder(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CancelOrder_ReturnsOk_WhenOrderIsPending()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "345/356",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 1000,
                OrderStatus = "Pending",
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CancelOrder(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var updatedOrder = await _context.Orders.FindAsync(1);
            Assert.AreEqual("Canceled", updatedOrder.OrderStatus);
        }

        [Test]
        public async Task CancelOrder_ReturnsBadRequest_WhenOrderIsAlreadyCanceled()
        {
            // Arrange
            var order = new Order
            {
                OrderId = 1,
                UserId = 1,
                OrderDate = DateTime.Now,
                Address = "345/356",
                PaymentMethod = "Paypal",
                ShippingMethodId = 1,
                Total = 1000,
                OrderStatus = "Canceled",
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CancelOrder(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CancelOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.CancelOrder(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }


    }
}
