using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ReviewControllerTests
    {
        private ReviewController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new ReviewController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetReviews_ReturnsOkResult_WithListOfReviews()
        {
            // Arrange
            _context.Reviews.AddRange(new List<Review>
            {
                new Review
                {
                    UserId = 1,
                    OrderDetailId = 1,
                    ProductId = 1,
                    Date = DateTime.Now,
                    Rating = 5,
                    Comment = "Great!",
                    IsRated = true
                },

                new Review
                {
                    UserId = 2,
                    OrderDetailId = 2,
                    ProductId = 2,
                    Date = DateTime.Now,
                    Rating = 4,
                    Comment = "Good",
                    IsRated = true
                }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetReviews();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var reviews = okResult.Value as List<ReviewDTO>;
            Assert.AreEqual(2, reviews.Count);
        }

        [Test]
        public async Task GetReviews_ReturnsNotFound_WhenNoReviewsExist()
        {
            // Act
            var result = await _controller.GetReviews();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetUserReviews_ReturnsOkResult_WithUserReviews()
        {
            // Arrange
            var userId = 1;
            var orderId = 1;
            _context.Reviews.Add(new Review
            {
                UserId = userId,
                OrderDetailId = orderId,
                ProductId = 1,
                Date = DateTime.Now,
                Rating = 5,
                Comment = "Great!",
                IsRated = true
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUserReviews(userId, orderId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var reviews = okResult.Value as List<Review>;
            Assert.AreEqual(1, reviews.Count);
        }

        [Test]
        public async Task GetUserReviews_ReturnsNotFound_WhenUserOrOrderNotExist()
        {
            // Act
            var result = await _controller.GetUserReviews(1, 1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task CreateRating_ReturnsOkResult_WhenReviewIsCreated()
        {
            // Arrange
            var reviewDto = new ReviewDTO
            {
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Rating = 5,
                Comment = "Excellent!"
            };

            // Act
            var result = await _controller.CreateRating(reviewDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task CreateRating_ReturnsBadRequest_WhenReviewDtoIsNull()
        {
            // Act
            var result = await _controller.CreateRating(null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task CreateRating_ReturnsBadRequest_WhenOrderDetailNotFound()
        {
            // Arrange
            var reviewDto = new ReviewDTO
            {
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Rating = 1,
                Comment = "Bad"
            };

            // Act
            var result = await _controller.CreateRating(reviewDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("OrderDetail not found", badRequestResult.Value);
        }

        [Test]
        public async Task GetProductRating_ReturnsOkResult_WithProductRatingDTO()
        {
            // Arrange
            var productId = 1;
            _context.Products.Add(new Product
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
            });

            _context.Reviews.AddRange(new List<Review>
            {
                new Review
                {
                    UserId = 1,
                    OrderDetailId = 1,
                    ProductId = productId,
                    Date = DateTime.Now,
                    Rating = 5,
                    Comment = "Excellent!"
                },

                new Review
                {
                    UserId = 2,
                    OrderDetailId = 2,
                    ProductId = productId,
                    Date = DateTime.Now,
                    Rating = 3,
                    Comment = "Average"
                }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = _controller.GetProductRating(productId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var productRatingDTO = okResult.Value as ReviewController.ProductRatingDTO;
            Assert.AreEqual(2, productRatingDTO.ReviewCount);
            Assert.AreEqual(8, productRatingDTO.TotalRating);
            Assert.AreEqual(4, productRatingDTO.AverageRating);
        }

        [Test]
        public async Task GetProductRating_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Act
            var result = _controller.GetProductRating(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateRating_ReturnsBadRequest_WhenOrderNotFound()
        {
            // Arrange
            var orderDetail = new OrderDetail
            {
                OrderDetailId = 1,
                OrderId = 1
            };

            _context.orderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            var reviewDto = new ReviewDTO
            {
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Rating = 1,
                Comment = "Bad"
            };

            // Act
            var result = await _controller.CreateRating(reviewDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Order not found", badRequestResult.Value);
        }

        [Test]
        public async Task CreateRating_SendsNotifications_WhenRatingIsLow()
        {
            // Arrange
            var orderDetail = new OrderDetail
            {
                OrderDetailId = 1,
                OrderId = 1
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
                OrderStatus = "Completed",
            };

            var staffUser = new User
            {
                UserId = 1,
                RoleId = 2,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = true,
            };

            _context.orderDetails.Add(orderDetail);
            _context.Orders.Add(order);
            _context.Users.Add(staffUser);
            await _context.SaveChangesAsync();

            var reviewDto = new ReviewDTO
            {
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Rating = 1,
                Comment = "Bad"
            };

            // Act
            var result = await _controller.CreateRating(reviewDto);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var notifications = _context.Notifications.ToList();
            Assert.AreEqual(1, notifications.Count);
            var notification = notifications.First();
            Assert.AreEqual(staffUser.UserId, notification.UserId);
            Assert.AreEqual($"Order {order.OrderId} just received a bad Rating. Please consider contacting the User to support!", notification.Content);
        }

        [Test]
        public async Task CreateRating_ReturnBadRequest_WhenOrderStatusIsNotCompleted()
        {
            // Arrange
            var orderDetail = new OrderDetail
            {
                OrderDetailId = 1,
                OrderId = 1
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
                OrderStatus = "Pending",
            };

            var staffUser = new User
            {
                UserId = 1,
                RoleId = 2,
                Name = "Test User",
                Email = "example@123",
                Password = "123",
                PhoneNumber = "123456789",
                Address = "345/356",
                IsActive = true,
            };

            _context.orderDetails.Add(orderDetail);
            _context.Orders.Add(order);
            _context.Users.Add(staffUser);
            await _context.SaveChangesAsync();

            var reviewDto = new ReviewDTO
            {
                UserId = 1,
                OrderDetailId = 1,
                ProductId = 1,
                Rating = 1,
                Comment = "Bad"
            };

            // Act
            var result = await _controller.CreateRating(reviewDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }

}

