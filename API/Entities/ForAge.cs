using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class ForAge
    {
        [Key]
        public int ForAgeId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
