﻿using API.Data;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(b => b.UserId == id);
            if (user != null)
            {
                var userDTO = AccountController.toUserDTO(user);
                return Ok(userDTO);
            }
            return NotFound();
        }

        [HttpPut("Update")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UpdateUserDTO userDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (userDto.Name == null || userDto.Email == null || userDto.PhoneNumber == null || userDto.Address == null)
            {
                return BadRequest();
            }

            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Address = userDto.Address;
            user.RoleId = userDto.RoleId;
            user.IsActive = userDto.IsActive;
            await _context.SaveChangesAsync();

            var updatedUserDto = AccountController.toUserDTO(user);

            return Ok(updatedUserDto);
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
                    VoucherId = o.VoucherId,
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
                    NameProduct = od.NameProduct,
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
                    Total = o.Total,
                    OrderStatus = o.OrderStatus, // Map OrderStatus property
                    VoucherId = o.VoucherId,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        ProductId = od.ProductId,
                        NameProduct = od.NameProduct,
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
                    VoucherId = o.VoucherId,
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

                if (order.OrderStatus == "Completed")
                {
                    return BadRequest("This Order has already Completed");
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

                if (order.OrderStatus == "Submited")
                {
                    return BadRequest("Order is already Submited, Cannot Cancel");
                }

                if (order.OrderStatus == "Canceled")
                {
                    return BadRequest("Order is already canceled");
                }

                //add notification
                var staffs = await _context.Users.Where(u => u.RoleId == 2).ToListAsync();
                if (order.OrderStatus == "Pre-Order")
                {
                    foreach (var staff in staffs)
                    {
                        var notification = NotificationExtensions.createNotification(staff.UserId, "Pre-Order Canceled!",
                            $"Pre-Order {order.OrderId} had been canceled. Please consider contact the User to support!");

                        _context.Notifications.Add(notification);
                    }
                }
                else
                {
                    foreach (var staff in staffs)
                    {
                        var notification = NotificationExtensions.createNotification(staff.UserId, "Order Canceled!",
                            $"Order {order.OrderId} had been canceled. Please consider contact the User to support!");

                        _context.Notifications.Add(notification);
                    }

                    //reset product's quantity
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var product = await _context.Products.FindAsync(orderDetail.ProductId);
                        if (product != null)
                        {
                            product.Stock += orderDetail.Quantity;
                        }
                    }
                }

                order.OrderStatus = "Canceled";
                await _context.SaveChangesAsync();

                return Ok("canceled successfully");
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
