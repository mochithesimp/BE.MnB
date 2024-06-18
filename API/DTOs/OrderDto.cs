using API.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OrderDto
    {

        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string? Address { get; set; }
        public string? PaymentMethod { get; set; }
        public int ShippingMethodId { get; set; }
        public int? VoucherId { get; set; }
        public double Total { get; set; }
        public List<OrderDetailDto> Products { get; set; }
    }
}
