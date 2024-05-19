using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("ShippingMethod")]
    public class ShippingMethod
    {
        [Key]
        public int ShippingMethodId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public float ShippingPrice { get; set; }

        public List<Order> Orders { get; set; }
    }
}
