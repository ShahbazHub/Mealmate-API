using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User User { get; set; }
    }
}
