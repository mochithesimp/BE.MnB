using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoucherControllerTest
{
    [TestFixture]
    public class VoucherControllerTests
    {
        private VoucherController _voucherController;
        private StoreContext _context;

        [SetUp]
        public void Setup()
        {
            // Initialize the controller with a mock database context
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            _context = new StoreContext(options); // Replace with your own StoreContext initialization logic

            _voucherController = new VoucherController(_context);
        }

        [Test]
        public async Task GetVouchers_ReturnsListOfVoucherDTOs()
        {
            // Arrange
            var vouchers = new List<Voucher>
            {
                new Voucher { Name = "Voucher1", Code = "CODE001" },
                new Voucher { Name = "Voucher2", Code = "CODE002" },
            };
            await _context.Vouchers.AddRangeAsync(vouchers);
            await _context.SaveChangesAsync();

            // Act
            var result = await _voucherController.GetVouchers();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.TypeOf<List<VoucherDTO>>());
            var voucherDTOs = okResult.Value as List<VoucherDTO>;
            Assert.That(voucherDTOs.Count, Is.EqualTo(vouchers.Count));
            Assert.That(voucherDTOs.Select(v => v.Name), Is.EquivalentTo(vouchers.Select(v => v.Name)));
            Assert.That(voucherDTOs.Select(v => v.Code), Is.EquivalentTo(vouchers.Select(v => v.Code)));
        }

        [Test]
        public async Task GetVouchers_NoVouchers_ReturnsNotFound()
        {
            // Act
            var result = await _voucherController.GetVouchers();

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task CreateVoucher_ValidVoucherDTO_ReturnsCreatedVoucher()
        {
            // Arrange
            var voucherDTO = new VoucherDTO
            {
                Name = "Voucher1",
                Code = "CODE001",
                DiscountType = "Percentage",
                DiscountValue = 10,
                MinimumTotal = 50,
                CreatedDate = DateTime.UtcNow,
                ExpDate = DateTime.UtcNow.AddDays(30),
                IsActive = true,
                ProductId = 1
            };

            // Act
            var result = await _voucherController.CreateVoucher(voucherDTO);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.TypeOf<Voucher>());
            var createdVoucher = okResult.Value as Voucher;
            Assert.That(createdVoucher.Name, Is.EqualTo(voucherDTO.Name));
            Assert.That(createdVoucher.Code, Is.EqualTo(voucherDTO.Code));
        }

        [Test]
        public async Task CreateVoucher_InvalidVoucherDTO_ReturnsBadRequest()
        {
            // Arrange
            var voucherDTO = new VoucherDTO(); // Empty voucher DTO

            // Act
            var result = await _voucherController.CreateVoucher(voucherDTO);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task UseVoucher_ExistingVoucherId_ReturnsDeactivatedVoucher()
        {
            // Arrange
            var voucher = new Voucher { Name = "Voucher1", Code = "CODE001", IsActive = true };
            await _context.Vouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();

            // Act
            var result = await _voucherController.UseVoucher(voucher.VoucherId);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult.Value, Is.TypeOf<object>());
            var voucherIdObj = okResult.Value as object;
            Assert.That(voucherIdObj, Is.Not.Null);
            Assert.That(voucherIdObj.GetType().GetProperty("voucherId"), Is.Not.Null);
            var voucherId = voucherIdObj.GetType().GetProperty("voucherId").GetValue(voucherIdObj);
            Assert.That(voucherId, Is.EqualTo(voucher.VoucherId));
        }

        [Test]
        public async Task UseVoucher_NonExistingVoucherId_ReturnsNotFound()
        {
            // Act
            var result = await _voucherController.UseVoucher(999);

// Assert
            Assert.That(result, Is.TypeOf<NotFoundObjectResult>());
            var notFoundResult = result as NotFoundObjectResult;
            Assert.That(notFoundResult.Value, Is.EqualTo("Voucher not found"));
        }
    }
    }
}