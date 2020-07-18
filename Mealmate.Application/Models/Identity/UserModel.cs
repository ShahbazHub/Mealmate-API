using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTimeOffset Created { get; set; }

        public IEnumerable<RestaurantModel> Restaurants { get; set; }

        public UserModel()
        {
        }
    }
}
