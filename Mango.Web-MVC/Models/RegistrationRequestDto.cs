using System.Text.Json.Serialization;

namespace Mango.Web_MVC.Models
{
    public class RegistrationRequestDto
    {
        public string Name { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
