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

    public class VoucherControllerTests
    {
        private VoucherController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new VoucherController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetVouchers_ReturnsOkResult_WithListOfVouchers()
        {
            // Arrange
            _context.Vouchers.AddRange(new List<Voucher>
            {
                new Voucher {
                    Name = "Voucher1",
                    Code = "Code1",
                    DiscountType = "%",
                    DiscountValue = 100,
                    MinimumTotal = 100000,
                    CreatedDate = DateTime.Now,
                    ExpDate = DateTime.Now,
                    ProductId = null,
                    IsActive = true,
                },



                new Voucher {
                    Name = "Voucher2",
                    Code = "Code2",
                    DiscountType = "%",
                    DiscountValue = 100,
                    MinimumTotal = 100000,
                    CreatedDate = DateTime.Now,
                    ExpDate = DateTime.Now,
                    ProductId = null,
                    IsActive = true, }
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetVouchers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var vouchers = okResult.Value as List<VoucherDTO>;
            Assert.AreEqual(2, vouchers.Count);
        }

        [Test]
        public async Task GetVouchers_ReturnsNotFound_WhenNoVouchersExist()
        {
            // Act
            var result = await _controller.GetVouchers();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateVoucher_ReturnsOkResult_WithCreatedVoucher()
        {
            // Arrange
            var voucherDto = new VoucherDTO
            {
                Name = "Voucher3",
                Code = "Code3",
                DiscountType = "%",
                DiscountValue = 100,
                MinimumTotal = 100000,
                CreatedDate = DateTime.Now,
                ExpDate = DateTime.Now,
                ProductId = null,
                IsActive = true,
            };

            // Act
            var result = await _controller.CreateVoucher(voucherDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var voucher = okResult.Value as Voucher;
            Assert.AreEqual(voucherDto.Name, voucher.Name);
        }

        [Test]
        public async Task CreateVoucher_ReturnsBadRequest_WhenVoucherDtoIsNull()
        {
            // Act
            var result = await _controller.CreateVoucher(null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        

        [Test]
        public async Task CreateVoucher_ReturnsBadRequest_WhenVoucherDtoHasMissingFields()
        {
            // Arrange
            var voucherDto = new VoucherDTO
            {

            };

            // Act
            var result = await _controller.CreateVoucher(voucherDto);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
        }

        
        
    }

}