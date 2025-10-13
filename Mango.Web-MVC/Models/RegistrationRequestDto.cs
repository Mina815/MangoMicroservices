using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mango.Web_MVC.Models
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
