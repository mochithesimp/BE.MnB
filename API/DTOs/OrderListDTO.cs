namespace API.DTOs
{
    public class OrderListDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public int ShippingMethodId { get; set; }
        public string OrderStatus { get; set; } 
        public int? VoucherId { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
