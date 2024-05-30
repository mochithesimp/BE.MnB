using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageBrandUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
