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

        //[HttpGet(Name = "GetOrder")]
        //public async Task<ActionResult<OrderDto>> GetOrder()
        //{
        //    var order = await RetrieveOrder();

        //    if(order == null) return NotFound();

        //    return
        //}

        //private async Task<Order> RetrieveOrder()
        //{
        //    return await _context.Orders
        //        .Include(i => i.OrderDetails)
        //        .ThenInclude(p => p.Product)
        //        .FirstOrDefaultAsync(x => x.UserId == Request.Cookies["userId"]);
        //}

        //private OrderDto MapOrderToDto(Order order)
        //{
        //    return new OrderDto
        //    {
        //        OrderId = order.OrderId,
        //        UserId = order.UserId,
        //        OrderDate = order.OrderDate,
        //        Address = order.Address,
        //        PaymentMethod = order.PaymentMethod,
        //        ShippingMethod = order.ShippingMethod,
        //        Total = order.Total,
        //        OrderStatus = order.OrderStatus,

        //        OrderDetails = order.OrderDetails.Select(item => new OrderDetailDto
        //        {
        //            ForAgeId = item.ForAgeId,
        //            CategoryId = item.Product.CategoryId,
        //            Price = item.Product.Price,
        //            PictureUrl = item.Product.PictureUrl,
        //            Type = item.Product.Type,
        //            Brand = item.Product.Brand,
        //            Quantity = item.Quantity
        //        }).ToList()
        //    };
        //}
    }
}
