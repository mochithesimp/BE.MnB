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
    public class BrandController : ControllerBase
    {
        private readonly StoreContext _context;

        public BrandController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var rs = await _context.Brands.ToListAsync();
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var rs = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == id);
            if(rs != null) { return Ok(rs); }
            return BadRequest();
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<Brand>> UpdateBrand(int id, BrandDTO updateBrand)
        {
            var oldBrand = await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == id);
            if(oldBrand == null) { return BadRequest(); }

            oldBrand.Name = updateBrand.Name;
            oldBrand.ImageBrandUrl = updateBrand.ImageBrandUrl;

            await _context.SaveChangesAsync();

            return Ok(oldBrand);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> CreateBrand(BrandDTO newBrand)
        {
            var brand = new Brand
            {
                Name = newBrand.Name,
                ImageBrandUrl = newBrand.ImageBrandUrl,
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBrand), new { id = brand.BrandId }, brand);
        }
    }
}
