using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
    }
}
