using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }


        public int RoleId { get; set; }


        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        [JsonIgnore]
        public string? Password { get; set; }


        public string? PhoneNumber { get; set; }


        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public Role? Role { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
