using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Created { get; set; }

        public IEnumerable<RestaurantModel> Restaurants { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RestaurantId { get; set; }
        public string Role { get; set; }

        public UserModel()
        {
        }

    }
}
