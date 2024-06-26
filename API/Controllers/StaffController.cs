﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using System.Transactions;

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
        public async Task<ActionResult<List<UserDTO>>> GetUsers(
                        string? searchName,
                        string? searchEmail,
                        int roleId,
                        string? orderBy
                        )
        {

            var query = _context.Users
                .SearchName(searchName)
                .SearchEmail(searchEmail)
                .FilterRole(roleId)
                .Sort(orderBy);

            var list = await query.ToListAsync();

            var users = new List<UserDTO>();
            foreach (var user in list.Where(u => u.RoleId == 1 || u.RoleId == 2))
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
        public async Task<ActionResult<List<Order>>> GetOrders(
                    int status, 
                    string? orderBy)
        {
            var querry = _context.Orders
                .FilterStatus(status)
                .Sort(orderBy);

            var list = await querry.ToListAsync();

            if (list.Count > 0) return Ok(list);
            return NotFound();
        }

        [HttpGet("GetAllRating")]
        public async Task<ActionResult<List<Review>>> GetReviews(
                    string? orderBy)
        {
            var query = _context.Reviews
                .FilterBy(orderBy);

            var list = await query.ToListAsync();

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
                    IsRated = item.IsRated,
                };
                reviews.Add(reviewDTO);
            }

            if (reviews.Count > 0) return Ok(reviews);
            return NotFound();
        }



        [HttpDelete("DeleteUser")]
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

        [HttpPut("submitOrder")]
        public async Task<IActionResult> SubmitOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId && o.OrderStatus != "Canceled");

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                if(order.OrderStatus == "Submited")
                {
                    return BadRequest("Order has already Submited");
                }

                if (order.OrderStatus == "Pre-Order")
                {

                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var product = await _context.Products.FindAsync(orderDetail.ProductId);

                        if (product == null)
                        {
                            return BadRequest("Product not found");
                        }

                        if (product.Stock < orderDetail.Quantity)
                        {
                            return BadRequest("Insufficient quantity in stock to fulfill the order");
                        }

                        product.Stock -= orderDetail.Quantity;
                    }

                    order.OrderStatus = "Submitted";
                    await _context.SaveChangesAsync();

                    var notificationPre = NotificationExtensions.createNotification(order.UserId, "Pre-Order Submitted",
                        $"Your recent Pre-Order with ID {order.OrderId} has been submitted!");

                    _context.Notifications.Add(notificationPre);
                    await _context.SaveChangesAsync();

                    return Ok("Pre-Order submitted successfully");
                }
                else
                {
                    order.OrderStatus = "Submitted";
                    await _context.SaveChangesAsync();

                    var notification = NotificationExtensions.createNotification(order.UserId, "Order Submitted",
                        $"Your recent order with ID {order.OrderId} has been submitted!");

                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();

                    return Ok("Order submitted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to cancel order. " + ex.InnerException?.Message);
            }
        }

        [HttpDelete("cancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                if(order.OrderStatus == "Canceled")
                {
                    return BadRequest("Order has already Canceled");
                }

                if (order.OrderStatus == "Pre-Order")
                {
                    var notification = NotificationExtensions.createNotification(order.UserId, "Pre-Order Canceled!",
                            $"Your Pre-Order ID: {order.OrderId} had been canceled. Please consider contact the staff to support!");

                    _context.Notifications.Add(notification);
                }
                else
                {
                    //reset product's quantity
                    foreach (var orderDetail in order.OrderDetails)
                    {
                        var product = await _context.Products.FindAsync(orderDetail.ProductId);
                        if (product != null)
                        {
                            product.Stock += orderDetail.Quantity;
                        }
                    }

                    var notification = NotificationExtensions.createNotification(order.UserId, "Order Canceled!",
                            $"Your Order ID: {order.OrderId} had been canceled. Please consider contact the staff to support!");

                    _context.Notifications.Add(notification);
                }

                order.OrderStatus = "Canceled";
                await _context.SaveChangesAsync();

                return Ok("Order canceled successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to cancel order. " + ex.InnerException?.Message);
            }
        }
    }
}
