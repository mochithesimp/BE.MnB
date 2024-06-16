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
    public class NotificationControllerTests
    {
        private NotificationController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new NotificationController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetNotisByUser_ReturnsOkResult_WithNotifications()
        {
            // Arrange
            var notifications = new List<Notification>
            {
                new Notification
                {
                    NotificationId = 1,
                    UserId = 1,
                    Header = "Header1",
                    Content = "Content1",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                },
                new Notification
                {
                    NotificationId = 2,
                    UserId = 1,
                    Header = "Header2",
                    Content = "Content2",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                }
            };

            _context.Notifications.AddRange(notifications);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetNotisByUser(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<NotificationDTO>>(okResult.Value);
            Assert.AreEqual(2, (okResult.Value as List<NotificationDTO>).Count);
        }

        [Test]
        public async Task GetNotisByUser_ReturnsNotFound_WhenNoNotificationsExist()
        {
            // Act
            var result = await _controller.GetNotisByUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task DeleteNotificationById_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var notification = new Notification
            {
                NotificationId = 1,
                UserId = 1,
                Header = "Header1",
                Content = "Content1",
                IsRead = false,
                IsRemoved = false,
                CreatedDate = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteNotificationById(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task DeleteNotificationById_ReturnsNotFound_WhenNotificationNotFound()
        {
            // Act
            var result = await _controller.DeleteNotificationById(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task DeleteNotificationsByUser_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var notifications = new List<Notification>
            {
                new Notification
                {
                    NotificationId = 1,
                    UserId = 1,
                    Header = "Header1",
                    Content = "Content1",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                },
                new Notification
                {
                    NotificationId = 2,
                    UserId = 1,
                    Header = "Header2",
                    Content = "Content2",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                }
            };

            _context.Notifications.AddRange(notifications);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteNotificationsByUser(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task DeleteNotificationsByUser_ReturnsNotFound_WhenNoNotificationsExist()
        {
            // Act
            var result = await _controller.DeleteNotificationsByUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test]
        public async Task UpdateNotificationReadStatus_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var notification = new Notification
            {
                NotificationId = 1,
                UserId = 1,
                Header = "Header1",
                Content = "Content1",
                IsRead = false,
                IsRemoved = false,
                CreatedDate = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateNotificationReadStatus(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var updatedNotification = await _context.Notifications.FindAsync(1);
            Assert.IsTrue(updatedNotification.IsRead);
        }

        [Test]
        public async Task UpdateNotificationReadStatus_ReturnsNotFound_WhenNotificationNotFound()
        {
            // Act
            var result = await _controller.UpdateNotificationReadStatus(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }
    }
}