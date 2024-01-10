using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Models.DTOs
{
    public class UserDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? Phone { get; set; }

        public int UserType { get; set; }

        public decimal Money { get; set; }
    }
}