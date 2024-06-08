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
    public class ReviewController : ControllerBase
    {
        private readonly StoreContext _context;

        public ReviewController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRating")]
        public async Task<ActionResult<List<Review>>> GetReviews()
        {
            var list = await _context.Reviews.ToListAsync();

            var reviews = new List<ReviewDTO>();
            foreach (var item in list.Select(review => review).ToList()) 
            {
                ReviewDTO reviewDTO = new ReviewDTO()
                {
                    UserId = item.UserId,
                    OrderDetailId = item.OrderDetailId,
                    ProductId = item.ProductId,
                    Date = item.Date,
                    Rating = item.Rating,
                    Comment = item.Comment,
                };
                reviews.Add(reviewDTO);
            }
            if (reviews.Count > 0) return Ok(reviews);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRating(ReviewDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null) return BadRequest();

                var rating = new Review
                {
                    UserId = reviewDTO.UserId,
                    OrderDetailId = reviewDTO.OrderDetailId,
                    ProductId = reviewDTO.ProductId,
                    Date = DateTime.Now,
                    Rating = reviewDTO.Rating,
                    Comment = reviewDTO.Comment,
                };

                _context.Reviews.Add(rating);
                await _context.SaveChangesAsync();

                return Ok(rating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to create review. " + ex.InnerException?.Message);
            }
        }

        [HttpPost("GetProductRating")]
        public ActionResult<ReviewDTO> GetProductRating(int productId)
        {
            try
            {
                var reviews = _context.Reviews
                    .Where(r => r.ProductId == productId)
                    .ToList();

                var reviewDTOs = reviews.Select(r => new ReviewDTO
                {
                    UserId = r.UserId,
                    OrderDetailId = r.OrderDetailId,
                    ProductId = r.ProductId,
                    Date = r.Date,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList();

                var product = _context.Products
                    .Include(p => p.Reviews)
                    .FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                {
                    return NotFound();
                }

                int reviewCount = product.Reviews.Count;
                int totalRating = product.Reviews.Sum(r => r.Rating);
                double averageRating = reviewCount > 0 ? (double)totalRating / reviewCount : 0;

                var productRatingDTO = new ProductRatingDTO
                {
                    ReviewDTOs = reviewDTOs,
                    ReviewCount = reviewCount,
                    TotalRating = totalRating,
                    AverageRating = averageRating
                };

                return Ok(productRatingDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the reviews.");
            }
        }





        private class ProductRatingDTO
        {
            public List<ReviewDTO> ReviewDTOs { get; set; }
            public int ReviewCount { get; set; }
            public int TotalRating { get; set; }
            public double AverageRating { get; set; }
        }
    }
}

