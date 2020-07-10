using System;

namespace Mealmate.Application.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
