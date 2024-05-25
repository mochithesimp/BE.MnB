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
        public DateTime OrderDate { get; set; }

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

        public void AddItem(Product product, int quantity)
        {
            if (OrderDetails.All(item => item.ProductId != product.ProductId))
            {
                OrderDetails.Add(new OrderDetail { Product = product, Quantity = quantity });
            }

            var existingItem = OrderDetails.FirstOrDefault(item => item.ProductId == product.ProductId);
            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(int productId, int quantity)
        {
            var item = OrderDetails.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity == 0) OrderDetails.Remove(item);
        }
    }
}
