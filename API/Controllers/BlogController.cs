using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;

        public BlogController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("getAllBlogs")]
        public async Task<ActionResult<List<Blog>>> GetBlogs()
        {
            var list = await _context.Blogs.ToListAsync();

            var blogs = new List<BlogDTO>();
            foreach (var blog in list.Select(blog => blog).ToList())
            {
                BlogDTO blogDTO = toBlogDTO(blog);
                blogs.Add(blogDTO);
            }
            if (blogs.Count > 0)
            {
                return Ok(blogs);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);


            if (blog == null ) return NotFound();

            var blogDTO = toBlogDTO(blog);

            return Ok(blogDTO);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BlogDTO>> CreateBlog([FromForm] BlogDTO blogDTO)
        {
            var blog = new Blog
            {
                Title = blogDTO.Title,
                Content = blogDTO.Content,
                ImageUrl = await SaveImage(blogDTO.ImageFile, blogDTO.BlogId),
                Author = blogDTO.Author,
                ProductId = blogDTO.ProductId,
                UploadDate = (DateTime)blogDTO.UploadDate,
                UpdateDate = (DateTime)blogDTO.UploadDate,
                View = 0,
                Like = 0,
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(blogDTO);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<BlogDTO>> UpdateBlog(int id,[FromForm] BlogDTO blogDTO)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Title = blogDTO.Title;
            blog.Content = blogDTO.Content;
            blog.Author = blogDTO.Author;
            blog.ProductId = blogDTO.ProductId;
            blog.UpdateDate = (DateTime)blogDTO.UpdateDate;
            if(blogDTO.ImageFile != null)
            {
            blog.ImageUrl = await SaveImage(blogDTO.ImageFile, blogDTO.BlogId);
            }

            await _context.SaveChangesAsync();

            var updatedBlogDTO = toBlogDTO(blog);
            return Ok(updatedBlogDTO);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteBlog(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetBlogLikeByUser")]
        public async Task<ActionResult<List<int>>> GetLikedBlogsByUser(int userId)
        {
            var likedBlogs = await _context.userBlogViews
                .Where(ubv => ubv.UserId == userId && ubv.Like > 0)
                .Select(ubv => ubv.BlogId)
                .ToListAsync();

            return Ok(likedBlogs);
        }

        [HttpPost("increaseView")]
        public async Task<IActionResult> IncreaseViewCount(int userId, int blogId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var hasViewedBlog = await _context.userBlogViews
                .AnyAsync(ubv => ubv.UserId == userId && ubv.BlogId == blogId);

            if (hasViewedBlog)
            {
                return BadRequest("The user has already viewed the blog.");
            }

            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog == null)
            {
                return NotFound();
            }

            blog.View++;

            var userBlogView = new UserBlogView
            {
                UserId = userId,
                BlogId = blogId
            };
            _context.userBlogViews.Add(userBlogView);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("increaseLike")]
        public async Task<IActionResult> IncreaseLikeCount(int userId, int blogId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userBlogView = await _context.userBlogViews
                .FirstOrDefaultAsync(ubv => ubv.UserId == userId && ubv.BlogId == blogId);

            if (userBlogView == null)
            {
                return BadRequest("The user has not viewed the blog.");
            }

            if (userBlogView.Like > 0)
            {
                return BadRequest("The user has already liked the blog.");
            }

            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Like++;
            userBlogView.Like++;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("cancelLike")]
        public async Task<IActionResult> CancelLike(int userId, int blogId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userBlogView = await _context.userBlogViews
                .FirstOrDefaultAsync(ubv => ubv.UserId == userId && ubv.BlogId == blogId);

            if (userBlogView == null)
            {
                return BadRequest("The user has not viewed the blog.");
            }

            if (userBlogView.Like == 0)
            {
                return BadRequest("The user has not liked the blog.");
            }

            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Like--;
            userBlogView.Like--;

            await _context.SaveChangesAsync();

            return Ok();
        }

        public static BlogDTO toBlogDTO(Blog? blog) 
        { 
            BlogDTO blogDTO = new BlogDTO();
            blogDTO.BlogId = blog.BlogId;
            blogDTO.Title = blog.Title;
            blogDTO.Content = blog.Content;
            blogDTO.ImageUrl = blog.ImageUrl;
            blogDTO.Author = blog.Author;
            blogDTO.ProductId = blog.ProductId;
            blogDTO.UpdateDate = blog.UpdateDate;
            blogDTO.UploadDate = blog.UploadDate;
            blogDTO.View = blog.View;
            blogDTO.Like = blog.Like;

            return blogDTO;
        }

        private async Task<string> SaveImage(IFormFile imageFile, int blogId)
        {
            string imageUrl = Path.GetFileNameWithoutExtension(imageFile.FileName).Replace(" ", "-");
            imageUrl = $"blog{blogId}-" + imageUrl + Path.GetExtension(imageFile.FileName);

            //thay đổi đường dẫn cho phù hợp
            var rootDirectory = _configuration["UploadSettings:UploadDirectory"];
            var targetDirectory = Path.Combine(rootDirectory, "blogs");

            // Đảm bảo thư mục tồn tại
            if (!Directory.Exists(targetDirectory))
            {
                throw new DirectoryNotFoundException($"The directory '{targetDirectory}' does not exist.");
            }

            // Tìm và xóa hình ảnh cũ
            var oldImagePattern = $"blog{blogId}-*";
            var oldImages = Directory.GetFiles(targetDirectory, oldImagePattern);
            foreach (var oldImage in oldImages)
            {
                System.IO.File.Delete(oldImage);
            }

            var imagePath = Path.Combine(targetDirectory, imageUrl);
            try
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed
                throw new Exception("An error occurred while saving the image.", ex);
            }
            return $"/images/blogs/" + imageUrl;
        }
    }
}
