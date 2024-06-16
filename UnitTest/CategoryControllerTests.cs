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
    public class CategoryControllerTests
    {
        private CategoryController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new StoreContext(_options);
            _controller = new CategoryController(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetCategories_ReturnsOkResult_WithActiveCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Category1", Description = "Description1", IsActive = true },
                new Category { CategoryId = 2, Name = "Category2", Description = "Description2", IsActive = false },
                new Category { CategoryId = 3, Name = "Category3", Description = "Description3", IsActive = true }
            };

            _context.Categories.AddRange(categories);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetCategories();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsInstanceOf<List<CategoryDTO>>(okResult.Value);

            var categoryList = okResult.Value as List<CategoryDTO>;
            Assert.AreEqual(2, categoryList.Count); // Only active categories should be returned
        }

        [Test]
        public async Task GetCategory_ReturnsOkResult_WithCategory()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Category1", Description = "Description1", IsActive = true };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetCategory(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetCategory_ReturnsNotFound_WhenCategoryNotFound()
        {
            // Act
            var result = await _controller.GetCategory(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateCategory_ReturnsCreatedAtActionResult_WithCategory()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDTO
            {
                Name = "Category1",
                Description = "Description1",
                IsActive = true
            };

            // Act
            var result = await _controller.CreateCategory(createCategoryDto);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);

            var createdResult = result.Result as CreatedAtActionResult;
            Assert.IsInstanceOf<CategoryDTO>(createdResult.Value);

            var categoryDto = createdResult.Value as CategoryDTO;
            Assert.AreEqual("Category1", categoryDto.Name);
        }

        [Test]
        public async Task UpdateCategory_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Category1", Description = "Description1", IsActive = true };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var updateCategoryDto = new CategoryDTO
            {
                CategoryId = 1,
                Name = "Updated Category",
                Description = "Updated Description",
                IsActive = true
            };

            // Act
            var result = await _controller.UpdateCategory(1, updateCategoryDto);

            // Assert
            Assert.IsInstanceOf<ActionResult<CategoryDTO>>(result);
        }
        [Test]
        public async Task UpdateCategory_ReturnsNotFound_WhenCategoryNotFound()
        {
            // Arrange
            var updateCategoryDto = new CategoryDTO
            {
                CategoryId = 1,
                Name = "Updated Category",
                Description = "Updated Description",
                IsActive = true
            };

            // Act
            var result = await _controller.UpdateCategory(1, updateCategoryDto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task DeleteCategory_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var category = new Category { CategoryId = 1, Name = "Category1", Description = "Description1", IsActive = true };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);

            var deletedCategory = await _context.Categories.FindAsync(1);
            Assert.IsFalse(deletedCategory.IsActive);
        }

        [Test]
        public async Task DeleteCategory_ReturnsNotFound_WhenCategoryNotFound()
        {
            // Act
            var result = await _controller.DeleteCategory(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
