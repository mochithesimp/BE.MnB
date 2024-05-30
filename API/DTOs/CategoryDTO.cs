using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class CategoryDTO
    {
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
