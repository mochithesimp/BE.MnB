using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public List<Product> Products { get; set; }
    }
}
