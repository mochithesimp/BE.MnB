using System.Security.Cryptography;
using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class AccountControllerTests
    {
        private AccountController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;
        private IConfiguration _configuration;
        private IRoleService _roleService;


        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            var configValues = new Dictionary<string, string>
            {
                { "Jwt:Key", "Taolavudeptraivodichbadaoquakhongcogicothelamkhoduoctao" },
                { "Jwt:Issuer", "https://localhost:7030/swagger/index.html" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();

            _controller = new AccountController(_context, _configuration, _roleService);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Login_ReturnsBadRequest_WithInvalidCredentials()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "nonexistent@example.com",
                Password = "wrongpassword"
            };

            // Act
            var result = await _controller.Login(loginDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task Register_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var registerDTO = new RegisterDTO
            {
                Email = "newuser@example.com",
                Password = "password",
                Name = "New User",
                Address = "New Address",
                PhoneNumber = "0987654321"
            };

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual("Create successfully", ((ResponseDTO)okResult.Value).Message);
        }

        [Test]
        public async Task Register_ReturnsBadRequest_WithDuplicateEmail()
        {
            // Arrange
            var user = new User
            {
                Email = "duplicate@example.com",
                Password = HashPassword("password"),
                Name = "Existing User",
                Address = "Existing Address",
                PhoneNumber = "1234567890"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var registerDTO = new RegisterDTO
            {
                Email = "duplicate@example.com",
                Password = "password",
                Name = "New User",
                Address = "New Address",
                PhoneNumber = "0987654321"
            };

            // Act
            var result = await _controller.Register(registerDTO);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("duplicate email", ((ResponseDTO)badRequestResult.Value).Message);
        }

        [Test]
        public async Task CheckMail_ReturnsOk_WhenEmailDoesNotExist()
        {
            // Arrange
            var email = "nonexistent@example.com";

            // Act
            var result = await _controller.checkMail(email);

            // Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public async Task CheckMail_ReturnsBadRequest_WhenEmailExists()
        {
            // Arrange
            var user = new User
            {
                Email = "existing@example.com",
                Password = HashPassword("password"),
                Name = "Existing User",
                Address = "Existing Address",
                PhoneNumber = "1234567890"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var email = "existing@example.com";

            // Act
            var result = await _controller.checkMail(email);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual("this mail exist in data", ((ResponseDTO)badRequestResult.Value).Message);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
