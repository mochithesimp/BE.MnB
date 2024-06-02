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

        public BlogController(StoreContext context)
        {
            _context = context;
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
            if(blogs.Count > 0)
            {
                return Ok(blogs);
            }
            return NotFound();
        }




        public static BlogDTO toBlogDTO(Blog? blog) 
        { 
            BlogDTO blogDTO = new BlogDTO();
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
    }
}
