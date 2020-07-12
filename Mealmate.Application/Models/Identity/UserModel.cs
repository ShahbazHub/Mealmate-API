using System;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Created { get; set; }

        public RestaurantModel Restaurant { get; set; }
    }
}
