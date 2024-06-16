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
    public class ChatControllerTests
    {
        private ChatController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new ChatController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetAllChatByUser_ReturnsOkResult_WithChats()
        {
            // Arrange
            var chats = new List<Chat>
            {
                new Chat
                {
                    ChatId = 1,
                    UserId = 1,
                    SenderId = 2,
                    Sender = "Sender1",
                    Content = "Message1",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                },
                new Chat
                {
                    ChatId = 2,
                    UserId = 1,
                    SenderId = 3,
                    Sender = "Sender2",
                    Content = "Message2",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                }
            };

            _context.Chats.AddRange(chats);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetAllChatByUser(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<ChatDTO>>(okResult.Value);
            Assert.AreEqual(2, (okResult.Value as List<ChatDTO>).Count);
        }

        [Test]
        public async Task GetAllChatByUser_ReturnsNotFound_WhenNoChatsExist()
        {
            // Act
            var result = await _controller.GetAllChatByUser(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        //[Test]
        //public async Task SendChatMessage_ReturnsOkResult_WhenSuccessful_SendToStaff()
        //{
        //    // Arrange
        //    var sender = new User
        //    {
        //        UserId = 1,
        //        Name = "Sender",
        //        RoleId = 1,
        //        Email = "123@",
        //        Password = "123"
        //    };

        //    var staff = new List<User>
        //    {
        //        new User { UserId = 2, Name = "Staff1", RoleId = 2, Email = "123@", Password = "123"},
        //        new User { UserId = 3, Name = "Staff2", RoleId = 2, Email = "123@", Password = "123" }
        //    };

        //    _context.Users.Add(sender);
        //    _context.Users.AddRange(staff);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.SendChatMessage(1, "Hello Staff", 0);

        //    // Assert
        //    Assert.IsInstanceOf<OkResult>(result);

        //    var chats = _context.Chats.Where(c => c.SenderId == 1).ToList();
        //    Assert.AreEqual(2, chats.Count);
        //}

        //[Test]
        //public async Task SendChatMessage_ReturnsOkResult_WhenSuccessful_SendToUser()
        //{
        //    // Arrange
        //    var sender = new User
        //    {
        //        UserId = 1,
        //        Name = "Sender",
        //        RoleId = 2,
        //        Email = "User1@",
        //        Password = "123"
        //    };

        //    var receiver = new User
        //    {
        //        UserId = 2,
        //        Name = "Receiver",
        //        RoleId = 3,
        //        Email = "User1@",
        //        Password = "123"
        //    };

        //    _context.Users.Add(sender);
        //    _context.Users.Add(receiver);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.SendChatMessage(1, "Hello User", 2);

        //    // Assert
        //    Assert.IsInstanceOf<OkResult>(result);

        //    var chats = _context.Chats.Where(c => c.SenderId == 1).ToList();
        //    Assert.AreEqual(1, chats.Count);
        //}

        //[Test]
        //public async Task SendChatMessage_ReturnsNotFound_WhenSenderNotFound()
        //{
        //    // Act
        //    var result = await _controller.SendChatMessage(1, "Hello", 2);

        //    // Assert
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public async Task SendChatMessage_ReturnsNotFound_WhenNoStaffUsersFound()
        //{
        //    // Arrange
        //    var sender = new User
        //    {
        //        UserId = 1,
        //        Name = "Sender",
        //        RoleId = 1,
        //        Email = "@123",
        //        Password = "234"
        //    };

        //    _context.Users.Add(sender);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.SendChatMessage(1, "Hello Staff", 0);

        //    // Assert
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        //[Test]
        //public async Task SendChatMessage_ReturnsNotFound_WhenReceiverNotFound()
        //{
        //    // Arrange
        //    var sender = new User
        //    {
        //        UserId = 1,
        //        Name = "Sender",
        //        RoleId = 2,
        //        Email = "@123",
        //        Password = "234"
        //    };

        //    _context.Users.Add(sender);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.SendChatMessage(1, "Hello User", 2);

        //    // Assert
        //    Assert.IsInstanceOf<NotFoundObjectResult>(result);
        //}

        [Test]
        public async Task UpdateIsReadChat_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var chats = new List<Chat>
            {
                new Chat
                {
                    ChatId = 1,
                    UserId = 1,
                    SenderId = 2,
                    Sender = "Sender1",
                    Content = "Message1",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                },
                new Chat
                {
                    ChatId = 2,
                    UserId = 1,
                    SenderId = 3,
                    Sender = "Sender2",
                    Content = "Message2",
                    CreatedDate = DateTime.Now,
                    IsRead = false
                }
            };

            _context.Chats.AddRange(chats);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateIsReadChat(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<ChatDTO>>(okResult.Value);
            var chatDTOs = okResult.Value as List<ChatDTO>;
            Assert.AreEqual(2, chatDTOs.Count);
            Assert.IsTrue(chatDTOs.All(c => c.IsRead));

            var updatedChats = _context.Chats.Where(c => c.UserId == 1).ToList();
            Assert.IsTrue(updatedChats.All(c => c.IsRead));
        }

        [Test]
        public async Task UpdateIsReadChat_ReturnsNotFound_WhenNoChatsExist()
        {
            // Act
            var result = await _controller.UpdateIsReadChat(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}

        
    
