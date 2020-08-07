using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int RestaurantId { get; set; }
        public List<string> Roles { get; set; }
        public UserCreateModel()
        {
            Roles = new List<string>();
        }
    }
}
