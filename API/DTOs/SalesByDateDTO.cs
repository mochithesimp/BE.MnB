namespace API.DTOs
{
    public class SalesByDateDTO
    {
        public DateTime Date { get; set; }
        public int ProductCount { get; set; }
        public double TotalPrice { get; set; }
    }
}
