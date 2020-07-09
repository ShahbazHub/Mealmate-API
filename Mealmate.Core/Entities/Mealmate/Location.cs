using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace Mealmate.Core.Entities
{
    public class Location : Entity
    {
        //public int LocationId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Table> Tables { get; set; }

        public Location()
        {
            Tables = new HashSet<Table>();
        }

    }
}