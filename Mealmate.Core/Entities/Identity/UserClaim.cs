using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}
