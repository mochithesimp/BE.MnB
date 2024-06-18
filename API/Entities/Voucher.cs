using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key]
        public int VoucherId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string DiscountType { get; set; }

        [Required]
        public int DiscountValue { get; set; }

        [Required]
        public double MinimumTotal { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        public List<Order> Orders { get; set; }

        public Product Product { get; set; }
    }
}
