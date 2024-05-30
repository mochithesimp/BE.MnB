using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly StoreContext _context;

        public StaffController(StoreContext context)
        {
            _context = context;

        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var list = await _context.Users.ToListAsync();

            var users = new List<UserDTO>();
            foreach (var user in list.Where(u => u.RoleId == 1))
            {
                UserDTO userDTO = AccountController.toUserDTO(user);
                users.Add(userDTO);
            }

            if (users.Count > 0)
                return Ok(users);
            else
                return NotFound();
        }

        [HttpGet("GetActiveUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetActiveUsers()
        {
            var list = await _context.Users.ToListAsync();

            var users = new List<UserDTO>();
            foreach (var user in list.Where(user => user.IsActive && user.RoleId == 1))
            {
                UserDTO userDTO = AccountController.toUserDTO(user);
                users.Add(userDTO);
            }

            if (users.Count > 0)
                return Ok(users);
            else
                return NotFound();
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<Order>>> GetOrders(int status)
        {
            var querry = _context.Orders.FilterStatus(status);

            var list = await querry.ToListAsync();

            if (list.Count > 0) return Ok(list);
            return NotFound();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id && u.IsActive && u.RoleId == 1);

            if (user == null)
            {
                return NotFound();
            }

            user.IsActive = false;
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return Ok();
            else
                return BadRequest(new ProblemDetails { Title = "Problem deleting user" });
        }
    }
}
