using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateImageProductDTO
    {
        public int ProductId { get; set; }

        public IFormFile? ImageFile { get; set; } // để lấy file ảnh về

    }
}
