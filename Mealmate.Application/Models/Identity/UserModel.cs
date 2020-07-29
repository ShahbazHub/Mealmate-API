using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserModel : UserBaseModel
    {
        public string Password { get; set; }
        public string Role { get; set; }
        public int RestaurantId { get; set; }

        [JsonIgnore]
        public IEnumerable<RestaurantModel> Restaurants { get; set; }

    }
}
