using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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

            //add order
            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderDate = orderDto.OrderDate,
                Address = orderDto.Address,
                PaymentMethod = orderDto.PaymentMethod,
                ShippingMethodId = orderDto.ShippingMethodId,
                OrderStatus = "Pending",
                VoucherId = orderDto.VoucherId,
                Total = orderDto.Total,
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            //add order detail
            bool checkPreOrder = false;
            foreach (var productInOrder in orderDto.Products)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productInOrder.ProductId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }
                if (product.Stock < productInOrder.Quantity)
                {
                    checkPreOrder = true;
                }
                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = productInOrder.ProductId,
                    Price = productInOrder.Price,
                    Quantity = productInOrder.Quantity,
                };
                _context.orderDetails.Add(orderDetail);

                if (!checkPreOrder)
                {
                    product.Stock -= productInOrder.Quantity;
                }
            }
            if (checkPreOrder) order.OrderStatus = "Pre-Order";

            //add notification
            var staffs = await _context.Users.Where(u => u.RoleId == 2).ToListAsync();
            if (checkPreOrder)
            {
                foreach (var staff in staffs)
                {
                    var notification = NotificationExtensions.createNotification(staff.UserId, "New Pre-Order!",
                        $"User {order.UserId} has placed a Pre-Order with ID {order.OrderId}! Please handle it!");

                    _context.Notifications.Add(notification);
                }
            }
            else
            {
                foreach (var staff in staffs)
                {
                    var notification = NotificationExtensions.createNotification(staff.UserId, "New Order!", 
                        $"User {order.UserId} has placed a Order with ID {order.OrderId}! Please confirm it!");

                    _context.Notifications.Add(notification);
                }
            }
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Failed to create order. " + ex.InnerException?.Message);
        }
    }

}