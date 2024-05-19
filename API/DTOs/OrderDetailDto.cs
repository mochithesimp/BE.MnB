using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OrderDetailDto
    {
        public int ForAgeId { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public bool IsActive { get; set; }
    }
}
