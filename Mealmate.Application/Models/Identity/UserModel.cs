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

        public List<RestaurantModel> Restaurants { get; set; }
        public List<BranchModel> Branches { get; set; }

        public string PhoneNumber { get; set; }
        //public string Password { get; set; }
        //public int RestaurantId { get; set; }
        public List<string> Roles { get; set; }

        public UserModel()
        {
            Restaurants = new List<RestaurantModel>();
            Roles = new List<string>();
        }

    }
}
