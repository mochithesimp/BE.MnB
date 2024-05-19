using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Avatar { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public Role Role { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Order> Orders { get; set; }
    }
}
