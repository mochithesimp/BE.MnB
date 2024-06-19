namespace API.DTOs.DashBoardDTOs
{
    public class TopUsersSummaryDTO
    {
        public List<UserOrderSummaryDTO> TopUsers { get; set; }
        public double TotalSumOfAllOrders { get; set; }
    }
}
