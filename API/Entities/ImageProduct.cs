using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("ImageProduct")]
    public class ImageProduct
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Product Product { get; set; }
    }
}
