namespace API.DTOs
{
    public class ReviewDTO
    {
        public int UserId { get; set; }

        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
