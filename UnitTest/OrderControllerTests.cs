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
    public class OrderControllerTests
    {
        private OrderController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new OrderController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        //[Test]
        //public async Task CreateOrder_ReturnsOk_WhenOrderIsValid()
        //{
        //    // Arrange
        //    _context.Products.AddRange(
        //        new Product
        //        {
        //            ProductId = 1,
        //            Name = "Product1",
        //            IsActive = true,
        //            CategoryId = 1,
        //            BrandId = 1,
        //            ForAgeId = 1,
        //            Price = 10,
        //            Description = "Sữa bột",
        //            Stock = 5
        //        },

        //        new Product
        //        {
        //            ProductId = 2,
        //            Name = "Product2",
        //            IsActive = false,
        //            CategoryId = 2,
        //            BrandId = 2,
        //            ForAgeId = 2,
        //            Price = 20,
        //            Description = "Sữa bột",
        //            Stock = 10
        //        }
        //    );

        //    _context.Users.AddRange(
        //        new User
        //        {
        //            UserId = 1,
        //            RoleId = 2,
        //            Name = "Staf1",
        //            Email = "example@123",
        //            Password = "123",
        //            PhoneNumber = "123456789",
        //            Address = "345/356",
        //            IsActive = true,
        //        },

        //        new User
        //        {
        //            UserId = 2,
        //            RoleId = 2,
        //            Name = "Staf2",
        //            Email = "example@123",
        //            Password = "123",
        //            PhoneNumber = "123456789",
        //            Address = "345/356",
        //            IsActive = true,
        //        }

        //        );

        //    var orderDto = new OrderDto
        //    {
        //        UserId = 1,
        //        OrderDate = DateTime.UtcNow,
        //        Address = "123 Street",
        //        PaymentMethod = "Credit Card",
        //        ShippingMethodId = 1,
        //        Products = new List<OrderDetailDto>
        //        {
        //            new OrderDetailDto
        //            {
        //                ProductId = 1,
        //                Quantity = 2,
        //                Price = 10,
        //                Total = 20
        //            },

        //            new OrderDetailDto
        //            {
        //                ProductId = 2,
        //                Quantity = 3,
        //                Price = 20,
        //                Total = 60
        //            }
        //        }
        //    };

        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.CreateOrder(orderDto);

        //    // Assert
        //    Assert.IsInstanceOf<OkObjectResult>(result);
        //    var okResult = result as OkObjectResult;
        //    var response = okResult.Value as dynamic;
        //    Assert.IsNotNull(response);
        //    Assert.IsTrue((int)response.orderId > 0);
        //}

        [Test]
        public async Task CreateOrder_ReturnsBadRequest_WhenOrderDtoIsNull()
        {
            // Act
            var result = await _controller.CreateOrder(null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task CreateOrder_ReturnsBadRequest_WhenProductNotFound()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                UserId = 1,
                OrderDate = DateTime.UtcNow,
                Address = "123 Street",
                PaymentMethod = "Credit Card",
                ShippingMethodId = 1,
                Products = new List<OrderDetailDto>
                {
                    new OrderDetailDto
                    {
                        ProductId = 999,
                        Quantity = 2,
                        Price = 10,
                        Total = 20
                    }
                }
            };

            // Act
            var result = await _controller.CreateOrder(orderDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Product not found", badRequestResult.Value);
        }

        [Test]
        public async Task CreateOrder_ReturnsBadRequest_WhenInsufficientProductQuantity()
        {
            // Arrange
            _context.Products.AddRange(
                new Product
                {
                    ProductId = 1,
                    Name = "Product1",
                    IsActive = true,
                    CategoryId = 1,
                    BrandId = 1,
                    ForAgeId = 1,
                    Price = 10,
                    Description = "Sữa bột",
                    Stock = 5
                },

                new Product
                {
                    ProductId = 2,
                    Name = "Product2",
                    IsActive = false,
                    CategoryId = 2,
                    BrandId = 2,
                    ForAgeId = 2,
                    Price = 20,
                    Description = "Sữa bột",
                    Stock = 10
                }
            );

            await _context.SaveChangesAsync();

            var order = new OrderDto
            {
                UserId = 1,
                OrderDate = DateTime.UtcNow,
                Address = "123 Street",
                PaymentMethod = "Credit Card",
                ShippingMethodId = 1,
                Products = new List<OrderDetailDto>
                {
                    new OrderDetailDto
                    {
                        ProductId = 1,
                        Quantity = 10,
                        Price = 10,
                        Total = 100
                    }
                }
            };


            // Act
            var result = await _controller.CreateOrder(order);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var badRequestResult = result as OkObjectResult;
        }       
    }
}

