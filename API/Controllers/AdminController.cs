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

        [HttpGet("getBestBuyer")]
        public async Task<ActionResult<List<UserDTO>>> GetBestBuyerUsers()
        {
            var orders = await _context.Orders.ToListAsync();

            var userTotals = orders
                .GroupBy(o => o.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    Total = g.Sum(o => o.Total)
                })
                .OrderByDescending(u => u.Total)
                .Take(5) 
                .ToList();

            var bestBuyerUserIds = userTotals.Select(u => u.UserId).ToList();

            var bestBuyerUsers = await _context.Users
                .Where(u => bestBuyerUserIds.Contains(u.UserId))
                .ToListAsync();

            if (bestBuyerUsers.Count > 0)
            {
                var bestBuyerUserDTOs = bestBuyerUsers.Select(user => AccountController.toUserDTO(user)).ToList();
                return Ok(bestBuyerUserDTOs);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getBestSellerProducts")]
        public async Task<ActionResult<List<ProductDTO>>> GetBestSellerProducts()
        {
            var orderDetails = await _context.orderDetails.ToListAsync();

            var productCounts = orderDetails
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(pc => pc.Count)
                .ToList();

            var bestSellerProductIds = productCounts
                .Take(10)
                .Select(pc => pc.ProductId)
                .ToList();

            var bestSellerProducts = await _context.Products
                .Where(p => bestSellerProductIds.Contains(p.ProductId))
                .ToListAsync();

            if (bestSellerProducts.Count > 0)
            {
                var bestSellerProductDTOs = new List<ProductDTO>();

                foreach (var product in bestSellerProducts)
                {
                    var imageProducts = await _context.ImageProducts
                        .Where(ip => ip.ProductId == product.ProductId)
                        .ToListAsync();

                    var imageProductDTOs = imageProducts.Select(ip => ProductsController.toImageDTO(ip)).ToList();

                    var productDTO = ProductsController.toProductDTO(product, imageProductDTOs);

                    bestSellerProductDTOs.Add(productDTO);
                }

                return Ok(bestSellerProductDTOs);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getTotalUser")]
        public async Task<ActionResult<int>> GetTotalUser()
        {
            var userCount = await _context.Users.CountAsync();
            return Ok(userCount);
        }

        [HttpGet("getTotalProduct")]
        public async Task<ActionResult<int>> GetTotalProduct()
        {
            var productCount = await _context.Products.CountAsync();
            return Ok(productCount);
        }

        [HttpGet("getTotalOrder")]
        public async Task<ActionResult<int>> getTotalOrder()
        {
            var orderCount = await _context.Orders.CountAsync();
            return Ok(orderCount);
        }

        [HttpGet("getTotalProfit")]
        public async Task<ActionResult<decimal>> getTotalProfit()
        {
            var totalOrderCount = await _context.Orders.SumAsync(o => o.Total);
            return Ok(totalOrderCount);
        }

        //[HttpGet("salesByDate")]
        //public async Task<ActionResult<List<SalesByDateDTO>>> GetSalesByDate()
        //{
        //    var salesByDate = await _context.orderDetails
        //        .GroupBy(od => od.Order.OrderDate.Date)
        //        .Select(g => new SalesByDateDTO
        //        {
        //            Date = g.Key,
        //            ProductCount = g.Sum(od => od.Quantity),
        //            TotalPrice = g.Sum(od => od.Quantity * od.Price)
        //        })
        //        .ToListAsync();

        //    return Ok(salesByDate);
        //}

        [HttpGet("salesByDate")]
        public async Task<ActionResult<List<SalesByDateDTO>>> GetSalesByDate()
        {
            var currentDate = DateTime.UtcNow.Date;
            var startDate = currentDate.AddDays(-6); // Start date is 7 days ago

            var salesByDate = await _context.orderDetails
                .Where(od => od.Order.OrderDate.Date >= startDate && od.Order.OrderDate.Date <= currentDate)
                .GroupBy(od => od.Order.OrderDate.Date)
                .Select(g => new SalesByDateDTO
                {
                    Date = g.Key,
                    ProductCount = g.Sum(od => od.Quantity),
                    TotalPrice = g.Sum(od => od.Quantity * od.Price)
                })
                .ToListAsync();

            return Ok(salesByDate);
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
