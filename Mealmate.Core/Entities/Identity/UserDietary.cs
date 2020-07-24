using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class UserDietary : Entity
    {
        public DateTimeOffset Created { get; set; }

        public int DietaryId { get; set; }
        public virtual Dietary Dietary { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsActive { get; set; }


        public UserDietary()
        {
        }
    }
}