using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class CreateImageProductDTO
    {
        public int ProductId { get; set; }

        public string ImageUrl { get; set; }
    }
}
