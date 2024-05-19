using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageBrandUrl { get; set; }
    }
}
