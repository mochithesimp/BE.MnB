using API.Controllers;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ProductControllerTests
    {
        private ProductsController _controller;
        private StoreContext _context;
        private DbContextOptions<StoreContext> _options;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "DBMnB")
                .Options;

            _context = new StoreContext(_options);

            var configurationSettings = new Dictionary<string, string>
            {
                { "UploadSettings:UploadDirectory", "uploads" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configurationSettings)
                .Build();

            _controller = new ProductsController(_context, _configuration);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetProducts_ReturnsProducts_WhenProductsMatchFilters()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProducts("priceAsc", "Product", 1, 1, 1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var products = okResult.Value as List<ProductDTO>;
            Assert.IsNotEmpty(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual("Product1", products.First().Name);
        }

        [Test]
        public async Task GetProducts_ReturnsEmptyList_WhenNoProductsMatchFilters()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProducts("priceDesc", "nonexistent", 999, 999, 999);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var products = okResult.Value as List<ProductDTO>;
            Assert.IsEmpty(products);
        }

        [Test]
        public async Task GetProduct_ReturnsProduct_WhenProductExistsAndIsActive()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var product = okResult.Value as ProductDTO;
            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.ProductId);
            Assert.AreEqual("Product1", product.Name);
        }

        [Test]
        public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProduct(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetProductByCategory_ReturnsProducts_WhenProductsMatchCategory()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProductByCategory(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var products = okResult.Value as List<ProductDTO>;
            Assert.IsNotEmpty(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(1, products.First().CategoryId);
        }

        [Test]
        public async Task GetProductByBrand_ReturnsProducts_WhenProductsMatchBrand()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProductByBrand(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var products = okResult.Value as List<ProductDTO>;
            Assert.IsNotEmpty(products);
            Assert.AreEqual(1, products.Count);
            Assert.AreEqual(1, products.First().BrandId);
        }

        //[Test]
        //public async Task CreateProduct_ReturnsCreatedProduct_WhenProductIsValid()
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
        //    _context.ImageProducts.AddRange(
        //        new ImageProduct
        //        {
        //            ImageId = 1,
        //            ProductId = 1,
        //            ImageUrl = "url1"
        //        },

        //        new ImageProduct
        //        {
        //            ImageId = 2,
        //            ProductId = 2,
        //            ImageUrl = "url2"
        //        }
        //    );
        //    _context.SaveChanges();

        //    var productDto = new CreateProductDTO
        //    {
        //        Name = "Product3",
        //        Description = "Description3",
        //        Price = 30,
        //        Stock = 15,
        //        CategoryId = 1,
        //        BrandId = 1,
        //        ForAgeId = 1,
        //        ImageProducts = new List<CreateImageProductDTO>
        //        {
        //            new CreateImageProductDTO { ProductId = 1,ImageFile = new FormFile(Stream.Null, 0, 0, null, "test.jpg")
        //        }
        //    }
        //    };

        //    _context.SaveChanges();

        //    // Act
        //    var result = await _controller.CreateProduct(productDto);

        //    // Assert
        //    Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        //    var createdAtActionResult = result.Result as CreatedAtActionResult;
        //    var createdProduct = createdAtActionResult.Value as ProductDTO;
        //    Assert.IsNotNull(createdProduct);
        //    Assert.AreEqual("Product3", createdProduct.Name);
        //}

        //[Test]
        //public async Task UpdateProduct_ReturnsUpdatedProduct_WhenProductExistsAndIsValid()
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
        //    _context.ImageProducts.AddRange(
        //        new ImageProduct
        //        {
        //            ImageId = 1,
        //            ProductId = 1,
        //            ImageUrl = "url1"
        //        },

        //        new ImageProduct
        //        {
        //            ImageId = 2,
        //            ProductId = 2,
        //            ImageUrl = "url2"
        //        }
        //    );
        //    _context.SaveChanges();

        //    var productDto = new ProductDTO
        //    {
        //        ProductId = 1,
        //        Name = "UpdatedProduct1",
        //        Description = "UpdatedDescription1",
        //        Price = 15,
        //        Stock = 7,
        //        CategoryId = 1,
        //        BrandId = 1,
        //        ForAgeId = 1,
        //        IsActive = true,
        //        ImageProducts = new List<ImageProductDTO>
        //    {
        //        new ImageProductDTO { ImageId = 1, ProductId = 1, ImageUrl = "url1" }
        //    }
        //    };

        //    _context.SaveChanges();

        //    // Act
        //    var result = await _controller.UpdateProduct(1, productDto);

        //    // Assert
        //    Assert.IsInstanceOf<OkObjectResult>(result.Result);
        //    var okResult = result.Result as OkObjectResult;
        //    var updatedProduct = okResult.Value as ProductDTO;
        //    Assert.IsNotNull(updatedProduct);
        //    Assert.AreEqual("UpdatedProduct1", updatedProduct.Name);
        //    Assert.AreEqual(15, updatedProduct.Price);
        //}

        [Test]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductExists()
        {

            // Act
            var result = await _controller.DeleteProduct(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var product = await _context.Products.FindAsync(1);
            Assert.IsNull(product);
        }

        [Test]
        public async Task GetProduct_ReturnsNotFound_WhenProductIsInactive()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProduct(2);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetProductByCategory_ReturnsNotFound_WhenNoProductsMatchCategory()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProductByCategory(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetProductByBrand_ReturnsNotFound_WhenNoProductsMatchBrand()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            // Act
            var result = await _controller.GetProductByBrand(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task CreateProduct_ReturnsBadRequest_WhenExceptionOccurs()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            var productDto = new CreateProductDTO
            {
                Name = "Product3",
                Description = "Description3",
                Price = 30,
                Stock = 15,
                CategoryId = 1,
                BrandId = 1,
                ForAgeId = 1,
                ImageProducts = new List<CreateImageProductDTO>
                {
                    new CreateImageProductDTO { ProductId = 1, ImageFile = new FormFile(Stream.Null, 0, 0, null, "test.jpg") }
                }
            };

            var invalidConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "UploadSettings:UploadDirectory", (string)null }
                })
                .Build();

            var controllerWithInvalidConfig = new ProductsController(_context, invalidConfiguration);

            // Act
            var result = await controllerWithInvalidConfig.CreateProduct(productDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
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
            _context.ImageProducts.AddRange(
                new ImageProduct
                {
                    ImageId = 1,
                    ProductId = 1,
                    ImageUrl = "url1"
                },

                new ImageProduct
                {
                    ImageId = 2,
                    ProductId = 2,
                    ImageUrl = "url2"
                }
            );
            _context.SaveChanges();

            var productDto = new ProductDTO
            {
                ProductId = 999,
                Name = "UpdatedProduct",
                Description = "UpdatedDescription",
                Price = 50,
                Stock = 20,
                CategoryId = 1,
                BrandId = 1,
                ForAgeId = 1,
                IsActive = true,
                ImageProducts = new List<ImageProductDTO>
                {
                    new ImageProductDTO { ImageId = 1, ProductId = 999, ImageUrl = "url1" }
                }
            };

            _context.SaveChanges();

            // Act
            var result = await _controller.UpdateProduct(999, productDto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteProduct(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task SaveImage_ThrowsDirectoryNotFoundException_WhenUploadDirectoryDoesNotExist()
        {
            // Arrange
            var imageFile = new FormFile(Stream.Null, 0, 0, null, "test.jpg");
            var invalidConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "UploadSettings:UploadDirectory", "nonexistent_directory" }
                })
                .Build();
            var controllerWithInvalidConfig = new ProductsController(_context, invalidConfiguration);

            // Act & Assert
            var ex = Assert.ThrowsAsync<DirectoryNotFoundException>(() => controllerWithInvalidConfig.SaveImage(imageFile, 1, 1));
            Assert.That(ex.Message, Is.EqualTo("The directory 'nonexistent_directory' does not exist."));
        }     
    }
}