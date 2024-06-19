namespace API.DTOs.DashBoardDTOs
{
    public class DailyOrderSummaryDTO
    {
        public DateTime OrderDate { get; set; }
        public int OrderCount { get; set; }
        public double TotalOrderAmount { get; set; }
    }
}
