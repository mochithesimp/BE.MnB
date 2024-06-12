using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ChatDTO
    {
        public int UserId { get; set; }

        public int SenderId { get; set; }

        public string Sender { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;
    }
}
