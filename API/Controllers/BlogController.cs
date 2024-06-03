﻿using API.Data;
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
            if (blogs.Count > 0)
            {
                return Ok(blogs);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BlogDTO>> GetBlogs(BlogDTO blogDTO)
        {
            var blog = new Blog
            {
                Title = blogDTO.Title,
                Content = blogDTO.Content,
                ImageUrl = blogDTO.ImageUrl,
                Author = blogDTO.Author,
                ProductId = blogDTO.ProductId,
                UploadDate = blogDTO.UploadDate,
                UpdateDate = blogDTO.UpdateDate,
                View = blogDTO.View,
                Like = blogDTO.Like,
            };

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<BlogDTO>> UpdateBlog(int blogId, BlogDTO blogDTO)
        {
            var blog = await _context.Blogs.FindAsync(blogId);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Title = blogDTO.Title;
            blog.Content = blogDTO.Content;
            blog.ImageUrl = blogDTO.ImageUrl;
            blog.Author = blogDTO.Author;
            blog.ProductId = blogDTO.ProductId;
            blog.UploadDate = blogDTO.UploadDate;
            blog.UpdateDate = blogDTO.UpdateDate;
            blog.View = blogDTO.View;
            blog.Like = blogDTO.Like;

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