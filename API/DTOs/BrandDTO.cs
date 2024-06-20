using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BrandDTO
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string ImageBrandUrl { get; set; }

        public bool IsActive { get; set; }

    }
}
