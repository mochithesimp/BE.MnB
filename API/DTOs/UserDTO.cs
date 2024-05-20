using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } = "***";

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
