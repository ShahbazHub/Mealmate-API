using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Password must matched")]
        public string ConfirmPassword { get; set; }
    }
}
