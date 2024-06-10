using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly StoreContext _context;

    public OrderController(StoreContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderDto orderDto)
    {
        try
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                Address = orderDto.Address,        // taken from web later
                PaymentMethod = orderDto.PaymentMethod, // taken from web later
                ShippingMethodId = orderDto.ShippingMethodId,
                OrderStatus = "Pending",
                Total = orderDto.Products.Sum(p => p.Total)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var productDto in orderDto.Products)
            {
                var product = await _context.Products.FindAsync(productDto.ProductId);

                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                if (product.Stock < productDto.Quantity)
                {
                    return BadRequest("Insufficient product quantity");
                }

                product.Stock -= productDto.Quantity;

                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    Price = productDto.Price
                };

                _context.orderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
            }

            var staffUser = await _context.Users.Where(u => u.RoleId == 2).ToListAsync();

            foreach (var user in staffUser)
            {
                var notification = new Notification
                {
                    UserId = user.UserId,
                    Header = "New Order!",
                    Content = $"User {user.UserId} has just placed an Order with ID {order.OrderId}! Please Confirm it!",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                };

                _context.Notifications.Add(notification);
            }

            await _context.SaveChangesAsync();

            return Ok(new { orderId = order.OrderId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Failed to create order. " + ex.InnerException?.Message);
        }
    }


    [HttpPost("PreOrder")]
    public async Task<IActionResult> CreatePreOrder(OrderDto orderDto)
    {
        try
        {
            if (orderDto == null)
            {
                return BadRequest();
            }

            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                Address = orderDto.Address,
                PaymentMethod = orderDto.PaymentMethod,
                ShippingMethodId = orderDto.ShippingMethodId,
                OrderStatus = "Pre-Order",
                Total = orderDto.Products.Sum(p => p.Total)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var productDto in orderDto.Products)
            {
                var product = await _context.Products.FindAsync(productDto.ProductId);

                if (product == null)
                {
                    return BadRequest("Product not found");
                }

                if(product.Stock >= productDto.Quantity)
                {
                    return BadRequest("Quantity in Stock can satisfy required quantity! Can not Pre-order!");
                }

                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    Price = productDto.Price
                };

                _context.orderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
            }

            var staffUser = await _context.Users.Where(u => u.RoleId == 2).ToListAsync();

            foreach (var user in staffUser)
            {
                var notification = new Notification
                {
                    UserId = user.UserId,
                    Header = "New Pre-Order!",
                    Content = $"User {user.UserId} has just Pre-Order with ID {order.OrderId}! Please add more Product and handle!",
                    IsRead = false,
                    IsRemoved = false,
                    CreatedDate = DateTime.Now
                };

                _context.Notifications.Add(notification);
            }

            return Ok(new { orderId = order.OrderId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Failed to create pre-order. " + ex.InnerException?.Message);
        }
    }


}