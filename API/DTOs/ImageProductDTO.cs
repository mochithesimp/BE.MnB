using System.ComponentModel.DataAnnotations;

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
    }
}
