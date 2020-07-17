using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IMenuItemAllergenRepository : IRepository<MenuItemAllergen>
    {
        Task<IPagedList<MenuItemAllergen>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<MenuItemAllergen>> SearchAsync(int menuItemId, PageSearchArgs args);
    }
}
