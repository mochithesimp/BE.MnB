using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string Header { get; set; }

        public string Content { get; set; }

        public bool IsRead { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
