using Mealmate.Application.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{

    public class MobileRegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        public List<UserAllergenCreateModel> UserAllergens { get; set; }
        public List<UserDietaryCreateModel> UserDietaries { get; set; }
    }
}
