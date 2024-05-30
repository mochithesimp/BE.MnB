using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StoreContext _context;

        public CategoryController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var list = await _context.Categories.ToListAsync();

            var categories = new List<CategoryDTO>();
            foreach (var category in list.Where(category => category.IsActive))
            {
                CategoryDTO categoryDTO = toCategoryDTO(category);
                categories.Add(categoryDTO);
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if(category == null || category.IsActive == false) 
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CreateCategoryDTO categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                IsActive = categoryDto.IsActive,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var createdCategoryDto = new CategoryDTO
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                IsActive = categoryDto.IsActive,
            };

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDto.CategoryId }, createdCategoryDto);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.IsActive = categoryDto.IsActive;

            await _context.SaveChangesAsync();

            var updateCategoryDto = new CategoryDTO
            {
                CategoryId = categoryDto.CategoryId,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                IsActive = categoryDto.IsActive,
            };
            return updateCategoryDto;
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.IsActive = false;
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting category" });
        }




        private static CategoryDTO toCategoryDTO(Category? category)
        {
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.CategoryId = category.CategoryId;
            categoryDTO.Name = category.Name;
            categoryDTO.Description = category.Description;

            return categoryDTO;
        }

    }
}
