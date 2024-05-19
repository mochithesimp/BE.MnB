using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public int ShippingMethodId { get; set; }

        public double Total { get; set; }

        public string OrderStatus { get; set; }

        public User User { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
