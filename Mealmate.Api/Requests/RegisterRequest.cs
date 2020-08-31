using Mealmate.Application.Models;

using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{

    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsRestaurantAdmin { get; set; } = false;
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
    }
}
