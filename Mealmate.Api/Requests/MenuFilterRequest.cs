using System.Collections.Generic;

namespace Mealmate.Api.Requests
{
    public class MenuFilterRequest
    {
        public List<int> allergenIds { get; set; }
        public List<int> dietaryIds { get; set; }

        public MenuFilterRequest()
        {
            allergenIds = new List<int>();
            dietaryIds = new List<int>();
        }
    }
}