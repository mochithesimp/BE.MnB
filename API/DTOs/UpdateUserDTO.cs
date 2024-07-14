using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        public int RoleId { get; set; }


        public string? Name { get; set; }


        public string? Email { get; set; }


        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}
