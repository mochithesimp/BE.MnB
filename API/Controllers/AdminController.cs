using API.Data;
using API.DTOs;
using API.DTOs.DashBoardDTOs;
using API.Entities;
using API.Extensions;
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
            var totalOrderCount = await _context.Orders.Where(o => o.OrderStatus != "Canceled").SumAsync(o => o.Total);
            return Ok(totalOrderCount);
        }

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

        [HttpGet("GetOrders")]
        public async Task<ActionResult> GetOrders()
        {
            var list = await _context.Orders
                .Where(order => order.OrderStatus == "Completed")
                .Select(order => new
                {
                    order.Total,
                    UserName = order.User.Name,
                    order.OrderDate
                })
                .ToListAsync();

            if (list.Count > 0) return Ok(list);
            return NotFound();
        }
        public class ProductWithOrderCountDTO
        {
            public ProductDTO Product { get; set; }
            public int OrderCount { get; set; }
        }

        [HttpGet("getBestSellerProducts")]
        public async Task<ActionResult<List<ProductWithOrderCountDTO>>> GetBestSellerProducts()
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
                .Take(5)
                .Select(pc => pc.ProductId)
                .ToList();

            var bestSellerProducts = await _context.Products
                .Where(p => bestSellerProductIds.Contains(p.ProductId))
                .ToListAsync();

            if (bestSellerProducts.Count > 0)
            {
                var bestSellerProductDTOs = new List<ProductWithOrderCountDTO>();

                foreach (var product in bestSellerProducts)
                {
                    var imageProducts = await _context.ImageProducts
                        .Where(ip => ip.ProductId == product.ProductId)
                        .ToListAsync();

                    var imageProductDTOs = imageProducts.Select(ip => ProductsController.toImageDTO(ip)).ToList();

                    var productDTO = ProductsController.toProductDTO(product, imageProductDTOs);

                    var orderCount = productCounts.First(pc => pc.ProductId == product.ProductId).Count;

                    var productWithOrderCountDTO = new ProductWithOrderCountDTO
                    {
                        Product = productDTO,
                        OrderCount = orderCount
                    };

                    bestSellerProductDTOs.Add(productWithOrderCountDTO);
                }

                return Ok(bestSellerProductDTOs);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTopBrandsByOrderCount")]
        public async Task<ActionResult<List<BrandOrderCountDTO>>> GetTopBrandsByOrderCount()
        {
            var orderDetails = await _context.orderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .ThenInclude(p => p.Brand)
                .ToListAsync();

            var brandOrderCounts = orderDetails
                .GroupBy(od => new { od.Product.BrandId, od.Order.PaymentMethod })
                .Select(g => new
                {
                    BrandId = g.Key.BrandId,
                    PaymentMethod = g.Key.PaymentMethod,
                    Count = g.Count(),
                    BrandName = g.First().Product.Brand.Name 
                })
                .GroupBy(g => g.BrandId)
                .Select(g => new BrandOrderCountDTO
                {
                    BrandId = g.Key,
                    BrandName = g.First().BrandName,
                    PaypalOrderCount = g.Where(x => x.PaymentMethod == "By Paypal").Sum(x => x.Count),
                    CashOrderCount = g.Where(x => x.PaymentMethod == "By Cash").Sum(x => x.Count)
                })
                .OrderByDescending(b => b.PaypalOrderCount + b.CashOrderCount)
                .Take(6)
                .ToList();

            if (brandOrderCounts.Count > 0)
            {
                return Ok(brandOrderCounts);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTopUsersByOrderCount")]
        public async Task<ActionResult<TopUsersSummaryDTO>> GetTopUsersByOrderCount()
        {
            var sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

            var orders = await _context.Orders
                .Include(o => o.User)
                .Where(o => o.OrderDate >= sevenDaysAgo)
                .ToListAsync();

            var userOrderGroups = orders
                .GroupBy(o => o.UserId)
                .Select(g => new UserOrderSummaryDTO
                {
                    UserId = g.Key,
                    UserName = g.First().User.Name,
                    OrderCount = g.Count(),
                    TotalOrderAmount = g.Sum(o => o.Total),
                    DailyOrderSummaries = g.GroupBy(o => o.OrderDate.Date)
                                           .Select(dg => new DailyOrderSummaryDTO
                                           {
                                               OrderDate = dg.Key,
                                               OrderCount = dg.Count(),
                                               TotalOrderAmount = dg.Sum(o => o.Total)
                                           })
                                           .OrderBy(d => d.OrderDate)
                                           .ToList()
                })
                .OrderByDescending(u => u.OrderCount)
                .Take(3)
                .ToList();

            var totalSumOfAllOrders = userOrderGroups.Sum(u => u.TotalOrderAmount);

            var result = new TopUsersSummaryDTO
            {
                TopUsers = userOrderGroups,
                TotalSumOfAllOrders = totalSumOfAllOrders
            };

            if (result.TopUsers.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }



        public static OrderDto toOrderDTO(Order? order)
        {
            OrderDto orderDTO = new OrderDto();
            orderDTO.UserId = order.UserId;
            orderDTO.OrderDate = order.OrderDate;
            orderDTO.Address = order.Address;
            orderDTO.PaymentMethod = order.PaymentMethod;
            orderDTO.ShippingMethodId = order.ShippingMethodId;
            orderDTO.VoucherId = order.VoucherId;

            return orderDTO;
        }

    }
}
