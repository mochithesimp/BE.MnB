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

namespace UnitTest
{
    public class UserControllerTests
    {
        private UserController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new UserController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUser_ReturnsUser_WhenUserExists()
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
            var result = await _controller.GetUser(user.UserId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var userDto = okResult.Value as UserDTO;
            Assert.AreEqual(user.UserId, userDto.UserId);
        }

        [Test]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.GetUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task UpdateUser_ReturnsUpdatedUser_WhenUserExists()
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

            var updateUser = new UpdateUserDTO
            {
                Name = "Update User",
                Email = "updateexample@123",
                PhoneNumber = "update123456789",
                Address = "update345/356",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateUser(user.UserId, updateUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var updatedUserDto = okResult.Value as UserDTO;
            Assert.AreEqual("Update User", updatedUserDto.Name);
            Assert.AreEqual("updateexample@123", updatedUserDto.Email);
            Assert.AreEqual("update123456789", updatedUserDto.PhoneNumber);
            Assert.AreEqual("update345/356", updatedUserDto.Address);
        }

        [Test]
        public async Task UpdateUser_ReturnsBadRequesr_WhenUpdateUserFieldEmpty()
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

            var updateUser = new UpdateUserDTO
            {
                Email = "updateexample@123",
                PhoneNumber = "update123456789",
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateUser(user.UserId, updateUser);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task UpdateUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var updateUser = new UpdateUserDTO
            {
                Name = "Update User",
                Email = "updateexample@123",
                PhoneNumber = "update123456789",
                Address = "update345/356",
            };

            // Act
            var result = await _controller.UpdateUser(1, updateUser);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetOrdersByUserId_ReturnsOrders_WhenUserExists()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrdersByUserId(user.UserId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var orders = okResult.Value as List<OrderDto>;
            Assert.AreEqual(1, orders.Count);
        }

        [Test]
        public async Task GetOrdersByUserId_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.GetOrdersByUserId(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetOrderDetailsByOrderId_ReturnsOrderDetails_WhenOrderExists()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                        OrderDetails = new List<OrderDetail>
                        {
                            new OrderDetail
                            {
                                OrderDetailId = 1,
                                ProductId = 1,
                                OrderId = 1,
                                Quantity = 1,
                                Price = 1000,
                            }
                        }

                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrderDetailsByOrderId(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var orderDetails = okResult.Value as List<OrderDetailDto>;
            Assert.AreEqual(1, orderDetails.Count);
        }

        [Test]
        public async Task GetOrderDetailsByOrderId_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.GetOrderDetailsByOrderId(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetOrderListByUserId_ReturnsOrderList_WhenUserExists()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                        OrderDetails = new List<OrderDetail>
                        {
                            new OrderDetail
                            {
                                OrderDetailId = 1,
                                ProductId = 1,
                                OrderId = 1,
                                Quantity = 1,
                                Price = 1000,
                            }
                        }

                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrderListByUserId(user.UserId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var orderList = okResult.Value as List<OrderListDTO>;
            Assert.AreEqual(1, orderList.Count);
        }

        [Test]
        public async Task GetOrderListByUserId_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.GetOrderListByUserId(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task GetOrderListByUserIdAndStatus_ReturnsFilteredOrders_WhenUserExists()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                    },

                    new Order
                    {
                        OrderId = 2,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "456/789",
                        PaymentMethod = "Cash",
                        ShippingMethodId = 2,
                        Total = 10000,
                        OrderStatus = "Completed",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrderListByUserIdAndStatus(user.UserId, 1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var orderList = okResult.Value as List<OrderListDTO>;
            Assert.AreEqual(1, orderList.Count);
            Assert.AreEqual("Pending", orderList[0].OrderStatus);
        }

        [Test]
        public async Task CompleteOrder_ReturnsOk_WhenOrderExists()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CompleteOrder(1, 1);

            // Assert
            var updatedOrder = _context.Orders.FirstOrDefault(o => o.OrderId == 1);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual("Completed", updatedOrder.OrderStatus);
        }

        [Test]
        public async Task CompleteOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.CompleteOrder(1, 1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task CompleteOrder_ReturnsBadRequest_WhenOrderIsAlreadyCompleted()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Completed",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CompleteOrder(1, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CancelOrder_ReturnsOk_WhenOrderIsExitsAndStatusIsPending()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Pending",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CancelOrder(1, 1);

            // Assert
            var updatedOrder = _context.Orders.FirstOrDefault(o => o.OrderId == 1);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual("Canceled", updatedOrder.OrderStatus);
        }

        [Test]
        public async Task CancelOrder_ReturnsBadRequest_WhenOrderExistAndStatusIsSubmited()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Submited",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CancelOrder(1, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }


        [Test]
        public async Task CancelOrder_ReturnsBadRequest_WhenOrderIsAlreadyCanceled()
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
                Orders = new List<Order>
                {
                    new Order
                    {
                        OrderId = 1,
                        UserId = 1,
                        OrderDate = DateTime.Now,
                        Address = "345/356",
                        PaymentMethod = "Paypal",
                        ShippingMethodId = 1,
                        Total = 1000,
                        OrderStatus = "Canceled",
                    }
                }
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.CancelOrder(1, 1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CancelOrder_ReturnsNotFound_WhenOrderDoesNotExist()
        {
            // Act
            var result = await _controller.CancelOrder(1, 1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

    }
}
