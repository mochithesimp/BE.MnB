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
    public class BrandControllerTests
    {
        private BrandController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            _controller = new BrandController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetBrands_ReturnsOkResult_WithBrands()
        {
            // Arrange
            _context.Brands.AddRange(new List<Brand>
                {
                    new Brand
                    {
                    BrandId = 1,
                    Name = "Brand1",
                    ImageBrandUrl = "url1",
                    IsActive = true,
                },

                new Brand
                {
                    BrandId = 2,
                    Name = "Brand2",
                    ImageBrandUrl = "url2",
                    IsActive = true,
                }
                });

            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBrands();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<Brand>>(okResult.Value);
            Assert.AreEqual(2, (okResult.Value as List<Brand>).Count);
        }

        [Test]
        public async Task GetBrands_ReturnsNotFound_WhenNoBrandsExist()
        {
            // Act
            var result = await _controller.GetBrands();

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetBrand_ReturnsOkResult_WithBrand()
        {
            //Arrange
            var brand = new Brand
            {
                BrandId = 1,
                Name = "Brand1",
                ImageBrandUrl = "url1",
                IsActive = true
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetBrand(brand.BrandId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<Brand>(okResult.Value);
            Assert.AreEqual(brand, okResult.Value);
        }

        [Test]
        public async Task GetBrand_ReturnsNotFoundResult_WhenBrandNotFound()
        {
            // Act
            var result = await _controller.GetBrand(1);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task UpdateBrand_ReturnsOkResult_WithUpdatedBrand()
        {
            // Arrange
            var brand = new Brand
            {
                BrandId = 1,
                Name = "Brand1",
                ImageBrandUrl = "url1",
                IsActive = true
            };

            var updateBrand = new BrandDTO
            {
                Name = "UpdatedBrand",
                ImageBrandUrl = "updatedUrl",
                IsActive = false
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateBrand(1, updateBrand);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<Brand>(okResult.Value);

            var updatedBrand = okResult.Value as Brand;
            Assert.AreEqual(updateBrand.Name, updatedBrand.Name);
            Assert.AreEqual(updateBrand.ImageBrandUrl, updatedBrand.ImageBrandUrl);
            Assert.AreEqual(updateBrand.IsActive, updatedBrand.IsActive);
        }

        [Test]
        public async Task UpdateBrand_ReturnsBadRequest_WhenBrandFieldMissing()
        {
            // Arrange
            var brand = new Brand
            {
                BrandId = 1,
                Name = "Brand1",
                ImageBrandUrl = "url1",
                IsActive = true
            };

            var updateBrand = new BrandDTO
            {
                IsActive = false
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.UpdateBrand(1, updateBrand);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task UpdateBrand_ReturnsBadRequest_WhenBrandNotFound()
        {
            // Arrange
            var updateBrand = new BrandDTO
            {
                Name = "UpdatedBrand",
                ImageBrandUrl = "updatedUrl",
                IsActive = false
            };

            // Act
            var result = await _controller.UpdateBrand(1, updateBrand);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task CreateBrand_ReturnsCreatedAtActionResult_WithNewBrand()
        {
            // Arrange
            var newBrand = new BrandDTO
            {
                Name = "NewBrand",
                ImageBrandUrl = "newUrl",
                IsActive = true
            };

            // Act
            var result = await _controller.CreateBrand(newBrand);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOf<Brand>(createdResult.Value);

            var createdBrand = createdResult.Value as Brand;
            Assert.AreEqual(newBrand.Name, createdBrand.Name);
            Assert.AreEqual(newBrand.ImageBrandUrl, createdBrand.ImageBrandUrl);
            Assert.AreEqual(true, createdBrand.IsActive);
        }

        [Test]
        public async Task CreateBrand_ReturnsBadRequestResult_WithNewBrandEmpty()
        {
            // Arrange
            var newBrand = new BrandDTO
            {
            };

            // Act
            var result = await _controller.CreateBrand(newBrand);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }

        [Test]
        public async Task DeleteBrand_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var brand = new Brand
            {
                BrandId = 1,
                Name = "Brand1",
                ImageBrandUrl = "url1",
                IsActive = true
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteBrand(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            Assert.IsFalse(brand.IsActive);
        }

        [Test]
        public async Task DeleteBrand_ReturnsNotFound_WhenBrandNotFound()
        {
            // Act
            var result = await _controller.DeleteBrand(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}

