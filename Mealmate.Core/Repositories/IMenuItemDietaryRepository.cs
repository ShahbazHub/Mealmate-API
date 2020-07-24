using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IMenuItemDietaryRepository : IRepository<MenuItemDietary>
    {
        Task<IPagedList<MenuItemDietary>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<MenuItemDietary>> SearchAsync(int menuItemId, int isActive, PageSearchArgs args);
    }
}
