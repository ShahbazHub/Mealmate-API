using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class ResetPasswordRequest
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
