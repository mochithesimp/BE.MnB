using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
