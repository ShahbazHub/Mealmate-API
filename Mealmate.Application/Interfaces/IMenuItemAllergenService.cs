using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemAllergenService
    {
        Task<IEnumerable<MenuItemAllergenModel>> Get(int menuItemId);
        Task<MenuItemAllergenModel> GetById(int id);
        Task<MenuItemAllergenModel> Create(MenuItemAllergenCreateModel model);
        Task Update(int id, MenuItemAllergenUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<MenuItemAllergenModel>> Search(PageSearchArgs args);
        Task<IPagedList<MenuItemAllergenModel>> Search(int menuItemId, int isActive, PageSearchArgs args);
    }
}
