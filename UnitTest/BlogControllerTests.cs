//using API.Controllers;
//using API.Data;
//using API.DTOs;
//using API.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UnitTest
//{
//    public class BlogControllerTests
//    {
//        private BlogController _controller;
//        private StoreContext _context;
//        private DbContextOptions<StoreContext> _options;

//        [SetUp]
//        public void Setup()
//        {
//            _options = new DbContextOptionsBuilder<StoreContext>()
//                .UseInMemoryDatabase(databaseName: "TestDB")
//                .Options;

//            _context = new StoreContext(_options);
//            _controller = new BlogController(_context);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            _context.Database.EnsureDeleted();
//            _context.Dispose();
//        }

//        [Test]
//        public async Task GetBlogs_ReturnsOkResult_WithBlogs()
//        {
//            // Arrange
//            var blogs = new List<Blog>
//            {
//                new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Author = "VLT", ImageUrl = "url" },
//                new Blog { BlogId = 2, Title = "Blog2", Content = "Content2", Author = "VLT", ImageUrl = "url" }
//            };

//            _context.Blogs.AddRange(blogs);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.GetBlogs();

//            // Assert
//            Assert.IsInstanceOf<OkObjectResult>(result.Result);

//            var okResult = result.Result as OkObjectResult;
//            Assert.IsInstanceOf<List<BlogDTO>>(okResult.Value);

//            var blogList = okResult.Value as List<BlogDTO>;
//            Assert.AreEqual(2, blogList.Count);
//        }

//        [Test]
//        public async Task GetBlog_ReturnsOkResult_WithBlog()
//        {
//            // Arrange
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Author = "VLT", ImageUrl = "url" };
//            _context.Blogs.Add(blog);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.GetBlog(1);

//            // Assert
//            Assert.IsInstanceOf<OkObjectResult>(result.Result);

//            var okResult = result.Result as OkObjectResult;
//            Assert.IsInstanceOf<BlogDTO>(okResult.Value);

//            var blogDto = okResult.Value as BlogDTO;
//            Assert.AreEqual("Blog1", blogDto.Title);
//        }

//        [Test]
//        public async Task GetBlog_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Act
//            var result = await _controller.GetBlog(1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result.Result);
//        }

//        [Test]
//        public async Task CreateBlog_ReturnsOkResult_WithBlog()
//        {
//            // Arrange
//            var createBlogDto = new BlogDTO
//            {
//                BlogId = 1,
//                Title = "Blog1",
//                Content = "Content1",
//                ImageUrl = "ImageUrl1",
//                Author = "Author1",
//                ProductId = 1,
//                UploadDate = System.DateTime.Now,
//                UpdateDate = System.DateTime.Now,
//                View = 0,
//                Like = 0
//            };

//            // Act
//            var result = await _controller.GetBlogs(createBlogDto);

//            // Assert
//            Assert.IsInstanceOf<OkObjectResult>(result.Result);

//            var okResult = result.Result as OkObjectResult;
//            Assert.IsInstanceOf<Blog>(okResult.Value);

//            var blog = okResult.Value as Blog;
//            Assert.AreEqual("Blog1", blog.Title);
//        }

//        [Test]
//        public async Task UpdateBlog_ReturnsOkResult_WithUpdatedBlog()
//        {
//            // Arrange
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Author = "VLT", ImageUrl = "url" };
//            _context.Blogs.Add(blog);
//            await _context.SaveChangesAsync();

//            var updateBlogDto = new BlogDTO
//            {
//                BlogId = 1,
//                Title = "Updated Blog",
//                Content = "Updated Content",
//                ImageUrl = "Updated ImageUrl",
//                Author = "Updated Author",
//                ProductId = 1,
//                UploadDate = System.DateTime.Now,
//                UpdateDate = System.DateTime.Now,
//                View = 0,
//                Like = 0
//            };

//            // Act
//            var result = await _controller.UpdateBlog(1, updateBlogDto);

//            // Assert
//            Assert.IsInstanceOf<OkObjectResult>(result.Result);

//            var okResult = result.Result as OkObjectResult;
//            Assert.IsInstanceOf<BlogDTO>(okResult.Value);

//            var updatedBlogDto = okResult.Value as BlogDTO;
//            Assert.AreEqual("Updated Blog", updatedBlogDto.Title);
//        }

//        [Test]
//        public async Task UpdateBlog_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Arrange
//            var updateBlogDto = new BlogDTO
//            {
//                BlogId = 1,
//                Title = "Updated Blog",
//                Content = "Updated Content",
//                ImageUrl = "Updated ImageUrl",
//                Author = "Updated Author",
//                ProductId = 1,
//                UploadDate = System.DateTime.Now,
//                UpdateDate = System.DateTime.Now,
//                View = 0,
//                Like = 0
//            };

//            // Act
//            var result = await _controller.UpdateBlog(1, updateBlogDto);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result.Result);
//        }

//        [Test]
//        public async Task DeleteBlog_ReturnsNoContent_WhenSuccessful()
//        {
//            // Arrange
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Author = "VLT", ImageUrl = "url" };
//            _context.Blogs.Add(blog);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.DeleteBlog(1);

//            // Assert
//            Assert.IsInstanceOf<NoContentResult>(result);

//            var deletedBlog = await _context.Blogs.FindAsync(1);
//            Assert.IsNull(deletedBlog);
//        }

//        [Test]
//        public async Task DeleteBlog_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Act
//            var result = await _controller.DeleteBlog(1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public async Task IncreaseViewCount_ReturnsOk_WhenSuccessful()
//        {
//            // Arrange
//            var user = new User { UserId = 1, Name = "User1", Email = "Vu@Vu", Password ="123" };
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", View = 0, Author = "VLT", ImageUrl = "url" };

//            _context.Users.Add(user);
//            _context.Blogs.Add(blog);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.IncreaseViewCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<OkResult>(result);

//            var updatedBlog = await _context.Blogs.FindAsync(1);
//            Assert.AreEqual(1, updatedBlog.View);
//        }

//        [Test]
//        public async Task IncreaseViewCount_ReturnsNotFound_WhenUserNotFound()
//        {
//            // Act
//            var result = await _controller.IncreaseViewCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public async Task IncreaseViewCount_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Arrange
//            var user = new User { UserId = 1, Name = "User1", Email = "Vu@Vu", Password = "123" };
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.IncreaseViewCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public async Task IncreaseLikeCount_ReturnsOk_WhenSuccessful()
//        {
//            // Arrange
//            var user = new User { UserId = 1, Name = "User1", Email = "Vu@vu", Password = "123" };
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Like = 0, Author = "VLT", ImageUrl = "url" };
//            var userBlogView = new UserBlogView { UserId = 1, BlogId = 1, Like = 0 };

//            _context.Users.Add(user);
//            _context.Blogs.Add(blog);
//            _context.userBlogViews.Add(userBlogView);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.IncreaseLikeCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<OkResult>(result);

//            var updatedBlog = await _context.Blogs.FindAsync(1);
//            Assert.AreEqual(1, updatedBlog.Like);

//            var updatedUserBlogView = await _context.userBlogViews
//                .FirstOrDefaultAsync(ubv => ubv.UserId == 1 && ubv.BlogId == 1);
//            Assert.AreEqual(1, updatedUserBlogView.Like);
//        }

//        [Test]
//        public async Task IncreaseLikeCount_ReturnsNotFound_WhenUserNotFound()
//        {
//            // Act
//            var result = await _controller.IncreaseLikeCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public async Task IncreaseLikeCount_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Arrange
//            var user = new User { UserId = 1, Name = "User1", Email = "Vu@vU", Password = "123" };
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.IncreaseLikeCount(1, 1);

//            // Assert
//            Assert.IsInstanceOf<BadRequestObjectResult>(result);
//        }

//        [Test]
//        public async Task CancelLike_ReturnsOk_WhenSuccessful()
//        {
//            // Arrange
//            var user = new User { UserId = 1, Name = "User1", Email = "Vu@vu", Password = "123" };
//            var blog = new Blog { BlogId = 1, Title = "Blog1", Content = "Content1", Like = 1, Author = "VLT", ImageUrl = "url" };
//            var userBlogView = new UserBlogView { UserId = 1, BlogId = 1, Like = 1 };

//            _context.Users.Add(user);
//            _context.Blogs.Add(blog);
//            _context.userBlogViews.Add(userBlogView);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.CancelLike(1, 1);

//            // Assert
//            Assert.IsInstanceOf<OkResult>(result);

//            var updatedBlog = await _context.Blogs.FindAsync(1);
//            Assert.AreEqual(0, updatedBlog.Like);

//            var updatedUserBlogView = await _context.userBlogViews
//                .FirstOrDefaultAsync(ubv => ubv.UserId == 1 && ubv.BlogId == 1);
//            Assert.AreEqual(0, updatedUserBlogView.Like);
//        }

//        [Test]
//        public async Task CancelLike_ReturnsNotFound_WhenUserNotFound()
//        {
//            // Act
//            var result = await _controller.CancelLike(1, 1);

//            // Assert
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public async Task CancelLike_ReturnsNotFound_WhenBlogNotFound()
//        {
//            // Arrange
//            var user = new User {UserId = 1, Name = "User1", Email = "Vu@vu", Password = "123" };
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            // Act
//            var result = await _controller.CancelLike(1, 1);

//            // Assert
//            Assert.IsInstanceOf<BadRequestObjectResult>(result);
//        }
//    }
//}