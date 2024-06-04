using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StoreContext _context;

        public UserController(StoreContext context)
        {
            _context = context;

        }

        [HttpPut("Update")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UpdateUserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Address = userDto.Address;

            await _context.SaveChangesAsync();

            var updatedUserDto = new UserDTO
            {
                UserId = user.UserId,
                RoleId = user.RoleId,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return updatedUserDto;
        }

        [HttpGet("getOrders")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Orders)
                                               .ThenInclude(o => o.OrderDetails)
                                               .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var orders = user.Orders.Select(o => new OrderDto
                {
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Address = o.Address,
                    PaymentMethod = o.PaymentMethod,
                    ShippingMethodId = o.ShippingMethodId,
                    Products = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                    }).ToList()
                }).ToList();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve orders. " + ex.InnerException?.Message);
            }
        }

        [HttpGet("getOrderDetails")]
        public async Task<IActionResult> GetOrderDetailsByOrderId(int orderId)
        {
            try
            {
                var order = await _context.Orders.Include(o => o.OrderDetails)
                                                 .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                var orderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Total = od.Price * od.Quantity,
                }).ToList();

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve order details. " + ex.InnerException?.Message);
            }
        }

        [HttpGet("getOrderList")]
        public async Task<IActionResult> GetOrderListByUserId(int userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Orders)
                                               .ThenInclude(o => o.OrderDetails)
                                               .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var orderList = user.Orders.Select(o => new OrderListDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Address = o.Address,
                    PaymentMethod = o.PaymentMethod,
                    ShippingMethodId = o.ShippingMethodId,
                    OrderStatus = o.OrderStatus, // Map OrderStatus property
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Total = od.Price * od.Quantity
                    }).ToList()
                }).ToList();

                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve order list. " + ex.InnerException?.Message);
            }
        }

        [HttpGet("getFilterOrderList")]
        public async Task<IActionResult> GetOrderListByUserIdAndStatus(int userId, int status)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Orders)
                                               .ThenInclude(o => o.OrderDetails)
                                               .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var filteredOrders = user.Orders.Where(o => FilterOrderStatus(o, status));

                var orderList = filteredOrders.Select(o => new OrderListDTO
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    Address = o.Address,
                    PaymentMethod = o.PaymentMethod,
                    ShippingMethodId = o.ShippingMethodId,
                    OrderStatus = o.OrderStatus,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Total = od.Price * od.Quantity
                    }).ToList()
                }).ToList();

                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve order list. " + ex.InnerException?.Message);
            }
        }


        [HttpPut("completeOrder")]
        public async Task<IActionResult> CompleteOrder(int userId, int orderId)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                order.OrderStatus = "Completed";
                await _context.SaveChangesAsync();

                return Ok("Order completed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to cancel order. " + ex.InnerException?.Message);
            }
        }

        [HttpDelete("cancelOrder")]
        public async Task<IActionResult> CancelOrder(int userId, int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderId == orderId);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                if (order.OrderStatus == "Canceled")
                {
                    return BadRequest("Order is already canceled");
                }

                order.OrderStatus = "Canceled";

                foreach (var orderDetail in order.OrderDetails)
                {
                    var product = await _context.Products.FindAsync(orderDetail.ProductId);
                    if (product != null)
                    {
                        product.Stock += orderDetail.Quantity;
                    }
                }

                await _context.SaveChangesAsync();

                return Ok("Order canceled successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to cancel order. " + ex.InnerException?.Message);
            }
        }



        public static bool FilterOrderStatus(Order order, int status)
        {
            if (status == 1)
            {
                return order.OrderStatus == "Pending";
            }

            if (status == 2)
            {
                return order.OrderStatus == "Submitted";
            }

            if (status == 3)
            {
                return order.OrderStatus == "Completed";
            }

            if (status == 4)
            {
                return order.OrderStatus == "Canceled";
            }

            return true;
        }


    }
}
