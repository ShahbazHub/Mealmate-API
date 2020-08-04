using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;
using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class UserOtp : Entity
    {
        public string Otp { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int UserId { get; set; }
        public virtual User User{ get; set; }

        public bool IsActive { get; set; }
    }
}
