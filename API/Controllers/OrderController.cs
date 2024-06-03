using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
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
            }

            await _context.SaveChangesAsync();

            return Ok(new { orderId = order.OrderId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Failed to create order. " + ex.InnerException?.Message);
        }
    }


}