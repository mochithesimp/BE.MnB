using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int ForAgeId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ForAge ForAge { get; set; }
        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<ImageProduct> ImageProducts { get; set; }
        public List<Review> Reviews { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
