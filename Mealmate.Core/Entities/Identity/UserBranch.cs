using System;

using Mealmate.Core.Entities.Base;

using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class UserBranch : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch{ get; set; }

        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; } 
    }
}
