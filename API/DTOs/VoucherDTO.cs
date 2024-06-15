using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class VoucherDTO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string DiscountType { get; set; }

        public int DiscountValue { get; set; }

        public double MinimumTotal { get; set; }

        public int? ProductId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
