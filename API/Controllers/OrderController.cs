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
    public class OrderController : ControllerBase
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //public async Task<ActionResult> CreateOrder(List<CartDTO> cart, UserDTO user)
        //{
        //    if (user == null) return BadRequest();
        //    if (cart == null) return BadRequest();

        //    return Ok(cart);
        //}
    }
}
