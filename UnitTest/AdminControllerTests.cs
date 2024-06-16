using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class AdminControllerTests
    {
        private AdminController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new AdminController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetUsers_ReturnsOkResult_WithUsers()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
            {
                new User { UserId = 1, Name = "User1", Email = "user1@example.com", RoleId = 1, IsActive = true, Password = "123"  },
                new User { UserId = 2, Name = "User2", Email = "user2@example.com", RoleId = 2, IsActive = true, Password = "123"  }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers(null, null, 0, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            Assert.AreEqual(2, (okResult.Value as List<UserDTO>).Count);
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
        public async Task GetActiveUsers_ReturnsOkResult_WithActiveUsers()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
            {
                new User { UserId = 1, Name = "User1", Email = "user1@example.com", IsActive = true, Password = "123"  },
                new User { UserId = 2, Name = "User2", Email = "user2@example.com", IsActive = false, Password = "123"  }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetActiveUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            Assert.AreEqual(1, (okResult.Value as List<UserDTO>).Count);
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
        public async Task GetOrders_ReturnsOkResult_WithOrders()
        {
            // Arrange
            _context.Orders.Add(new Order { OrderId = 1, UserId = 1, Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetOrders();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<Order>>(okResult.Value);
            Assert.AreEqual(1, (okResult.Value as List<Order>).Count);
        }

        [Test]
        public async Task GetOrders_ReturnsNotFound_WhenNoOrdersExist()
        {
            // Act
            var result = await _controller.GetOrders();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task DeleteUser_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var user = new User { UserId = 1, Name = "User1", Email = "Vu@vu", Password = "123", IsActive = true };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteUser(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetUsers_WithFilter_ReturnsFilteredUsers()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
            {
                new User { UserId = 1, Name = "User1", Email = "user1@example.com", RoleId = 1, IsActive = true, Password = "123" },
                new User { UserId = 2, Name = "User2", Email = "user2@example.com", RoleId = 2, IsActive = true, Password = "123" }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers("User1", null, 0, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            var users = okResult.Value as List<UserDTO>;
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("User1", users.First().Name);
        }

        [Test]
        public async Task GetUsers_WithSorting_ReturnsSortedUsers()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
            {
                new User { UserId = 1, Name = "Name", Email = "Name@example.com", RoleId = 1, IsActive = true, Password = "123"  },
                new User { UserId = 2, Name = "User2", Email = "Name1@example.com", RoleId = 1, IsActive = true, Password = "123"  }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers(null, "Name", 1, "name");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            var users = okResult.Value as List<UserDTO>;
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("Name", users.First().Name);
            Assert.AreEqual("User2", users.Last().Name);
        }

        [Test]
        public async Task GetUsers_WithPagination_ReturnsPaginatedUsers()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
            {
                new User { UserId = 1, Name = "User1", Email = "user1@example.com", RoleId = 1, IsActive = true, Password = "123"  },
                new User { UserId = 2, Name = "User2", Email = "user2@example.com", RoleId = 1, IsActive = true, Password = "123"  },
                new User { UserId = 3, Name = "User3", Email = "user3@example.com", RoleId = 2, IsActive = true, Password = "123"  }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetUsers(null, null, 1, "name");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            var users = okResult.Value as List<UserDTO>;
            Assert.AreEqual(2, users.Count);
            Assert.AreEqual("User1", users.First().Name);
            Assert.AreEqual("User2", users.Last().Name);
        }

        [Test]
        public async Task GetBestBuyerUsers_ReturnsOkResult_WithBestBuyers()
        {
            // Arrange
            var users = new List<User>
        {
            new User { UserId = 1, Name = "User1", Email = "user1@example.com", Password = "123" },
            new User { UserId = 2, Name = "User2", Email = "user2@example.com", Password = "123" }
        };
            _context.Users.AddRange(users);

            var orders = new List<Order>
        {
            new Order { OrderId = 1, UserId = 1, Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal" },
            new Order { OrderId = 2, UserId = 2, Total = 200, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  }
        };
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBestBuyerUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<UserDTO>>(okResult.Value);
            var bestBuyers = okResult.Value as List<UserDTO>;
            Assert.AreEqual(2, bestBuyers.Count);
        }

        [Test]
        public async Task GetBestBuyerUsers_ReturnsNotFound_WhenNoOrdersExist()
        {
            // Act
            var result = await _controller.GetBestBuyerUsers();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetBestSellerProducts_ReturnsOkResult_WithBestSellers()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Product1", Price = 10, Description = "Bot" },
            new Product { ProductId = 2, Name = "Product2", Price = 20, Description = "Bot" }
        };
            _context.Products.AddRange(products);

            var orderDetails = new List<OrderDetail>
        {
            new OrderDetail { OrderDetailId = 1, ProductId = 1, Quantity = 2, Price = 10 },
            new OrderDetail { OrderDetailId = 2, ProductId = 2, Quantity = 3, Price = 20}
        };
            _context.orderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBestSellerProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<ProductDTO>>(okResult.Value);
            var bestSellers = okResult.Value as List<ProductDTO>;
            Assert.AreEqual(2, bestSellers.Count);
        }

        [Test]
        public async Task GetBestSellerProducts_ReturnsNotFound_WhenNoOrderDetailsExist()
        {
            // Act
            var result = await _controller.GetBestSellerProducts();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetTotalUser_ReturnsOkResult_WithUserCount()
        {
            // Arrange
            _context.Users.AddRange(new List<User>
        {
            new User { UserId = 1, Name = "User1", Email = "user1@example.com", Password = "123" },
            new User { UserId = 2, Name = "User2", Email = "user2@example.com", Password = "123" }
        });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetTotalUser();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(2, okResult.Value);
        }

        [Test]
        public async Task GetTotalProduct_ReturnsOkResult_WithProductCount()
        {
            // Arrange
            _context.Products.AddRange(new List<Product>
        {
            new Product { ProductId = 1, Name = "Product1", Price = 10, Description = "Bot" },
            new Product { ProductId = 2, Name = "Product2", Price = 20, Description = "Bot" }});
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetTotalProduct();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(2, okResult.Value);
        }

        [Test]
        public async Task GetTotalOrder_ReturnsOkResult_WithOrderCount()
        {
            // Arrange
            _context.Orders.AddRange(new List<Order>
        {
            new Order { OrderId = 1, UserId = 1, Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  },
            new Order { OrderId = 2, UserId = 2, Total = 200, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  }
        });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.getTotalOrder();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(2, okResult.Value);
        }

        [Test]
        public async Task GetTotalProfit_ReturnsOkResult_WithTotalProfit()
        {
            // Arrange
            _context.Orders.AddRange(new List<Order>
        {
            new Order { OrderId = 1, UserId = 1, Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  },
            new Order { OrderId = 2, UserId = 2, Total = 200, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  }
        });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.getTotalProfit();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(300.0m, okResult.Value);
        }

        [Test]
        public async Task GetSalesByDate_ReturnsOkResult_WithSalesData()
        {
            // Arrange
            var currentDate = DateTime.UtcNow.Date;
            var orders = new List<Order>
        {
            new Order { OrderId = 1, UserId = 1, OrderDate = currentDate.AddDays(-1), Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  },
            new Order { OrderId = 2, UserId = 2, OrderDate = currentDate, Total = 200, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  }
        };
            _context.Orders.AddRange(orders);

            var orderDetails = new List<OrderDetail>
        {
            new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 1, Quantity = 1, Price = 100 },
            new OrderDetail { OrderDetailId = 2, OrderId = 2, ProductId = 2, Quantity = 2, Price = 100 }
        };
            _context.orderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetSalesByDate();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<SalesByDateDTO>>(okResult.Value);
            var salesData = okResult.Value as List<SalesByDateDTO>;
            Assert.AreEqual(2, salesData.Count);
        }

        [Test]
        public async Task GetSalesByDate_ReturnsEmptyList_WhenNoSalesInDateRange()
        {
            // Arrange
            var pastDate = DateTime.UtcNow.Date.AddDays(-10);
            var orders = new List<Order>
        {
            new Order { OrderId = 1, UserId = 1, OrderDate = pastDate, Total = 100, Address = "123", OrderStatus = "Pending", PaymentMethod = "Paypal"  }
        };
            _context.Orders.AddRange(orders);

            var orderDetails = new List<OrderDetail>
        {
            new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 1, Quantity = 1, Price = 100 }
        };
            _context.orderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetSalesByDate();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var salesData = okResult.Value as List<SalesByDateDTO>;
            Assert.AreEqual(0, salesData.Count);
        }
    }
}
 
