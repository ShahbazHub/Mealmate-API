using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class GoogleLoginRequest
    {
        //[Required]
        //public string AccessToken { get; set; }
        [Required]
        public string IdToken { get; set; }
        public string RegistrationToken { get;  set; }
        public string ClientId { get;  set; }
    }
}
