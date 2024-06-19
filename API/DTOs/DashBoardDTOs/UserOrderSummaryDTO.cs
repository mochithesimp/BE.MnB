namespace API.DTOs.DashBoardDTOs
{
    public class UserOrderSummaryDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OrderCount { get; set; }
        public double TotalOrderAmount { get; set; }
        public List<DailyOrderSummaryDTO> DailyOrderSummaries { get; set; }
    }
}
