using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IUserAllergenRepository : IRepository<UserAllergen>
    {
        Task<IPagedList<UserAllergen>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<UserAllergen>> SearchAsync(int userId, int isActive, PageSearchArgs args);
    }
}
