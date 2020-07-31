using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class FacebookLoginRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
