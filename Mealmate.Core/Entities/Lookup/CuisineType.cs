
using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class CuisineType : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}