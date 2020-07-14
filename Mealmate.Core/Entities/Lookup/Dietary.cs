using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities.Lookup
{
    public class Dietary : Entity
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }

        public DateTimeOffset Created { get; set; }

    }
}
