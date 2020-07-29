using Mealmate.Application.Models;

using System.ComponentModel.DataAnnotations;

namespace Mealmate.Api.Requests
{
    public class RegisterRequest : UserBaseModel
    {
        [Required]
        public string Password { get; set; }

        public bool IsRestaurantAdmin { get; set; } = false;
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
    }
}