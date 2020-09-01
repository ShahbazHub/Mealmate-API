using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }

        public string ClientId { get; set; }
        public string RegistrationToken { get; set; }
    }
}
