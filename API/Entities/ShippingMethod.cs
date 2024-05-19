using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
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
