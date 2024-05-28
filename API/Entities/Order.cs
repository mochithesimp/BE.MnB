using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string Address { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public int ShippingMethodId { get; set; }

        [Required]
        public double Total { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        public User? User { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}