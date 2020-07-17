using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IMenuItemOptionRepository : IRepository<MenuItemOption>
    {
        Task<IPagedList<MenuItemOption>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<MenuItemOption>> SearchAsync(int menuItemId, PageSearchArgs args);
    }
}
