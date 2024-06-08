using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        public int ForAgeId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public List<CreateImageProductDTO>? ImageProducts { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
