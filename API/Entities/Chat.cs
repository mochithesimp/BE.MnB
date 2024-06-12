using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Chat")]
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string Sender {  get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsRead { get; set; } = false;

        public User User { get; set; }
    }
}
