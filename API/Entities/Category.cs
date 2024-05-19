using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
