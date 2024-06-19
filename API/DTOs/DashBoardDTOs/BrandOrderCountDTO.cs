namespace API.DTOs.DashBoardDTOs
{
    public class BrandOrderCountDTO
    { 
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int PaypalOrderCount { get; set; }
        public int CashOrderCount { get; set; }
    }
}
