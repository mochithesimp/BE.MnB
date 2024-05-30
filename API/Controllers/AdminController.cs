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
    public class AdminController : ControllerBase
    {
        private readonly StoreContext _context;

        public AdminController (StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var list = await _context.Users.ToListAsync();

            var users = new List<UserDTO>();
            foreach (var user in list.Select(user => user).ToList())
            {
                UserDTO userDTO = AccountController.toUserDTO(user);
                users.Add(userDTO);
            }
            if(users.Count > 0) return Ok(users);
            return NotFound();
        }

        [HttpGet("GetActiveUsers")]
        public async Task<ActionResult<List<User>>> GetActiveUsers()
        {
            var list = await _context.Users.ToListAsync();

            var users = new List<UserDTO>();
            foreach (var user in list.Where(user => user.IsActive).ToList())
            {
                UserDTO userDTO = AccountController.toUserDTO(user);
                users.Add(userDTO);
            }
            if (users.Count > 0) return Ok(users);
            return NotFound();
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var list = await _context.Orders.ToListAsync();

            if(list.Count > 0) return Ok(list);
            return NotFound();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting user" });
        }



        public static OrderDto toOrderDTO(Order? order)
        {
            OrderDto orderDTO = new OrderDto();
            orderDTO.UserId = order.UserId;
            orderDTO.OrderDate = order.OrderDate;
            orderDTO.Address = order.Address;
            orderDTO.PaymentMethod = order.PaymentMethod;
            orderDTO.ShippingMethodId = order.ShippingMethodId;

            return orderDTO;
        }

    }
}
