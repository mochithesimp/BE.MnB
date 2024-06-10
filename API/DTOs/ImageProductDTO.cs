using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class ImageProductDTO
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // để lấy file ảnh về
    }
}
