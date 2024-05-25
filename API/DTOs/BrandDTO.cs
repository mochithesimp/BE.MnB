using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class BrandDTO
    {
        [Key]
        [JsonIgnore]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageBrandUrl { get; set; }
    }
}
