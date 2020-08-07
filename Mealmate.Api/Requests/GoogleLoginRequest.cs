using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class GoogleLoginRequest
    {
        //[Required]
        //public string AccessToken { get; set; }
        [Required]
        public string IdToken { get; set; }
    }
}
