using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemDietaryService
    {
        Task<IEnumerable<MenuItemDietaryModel>> Get(int menuItemId);
        Task<MenuItemDietaryModel> GetById(int id);
        Task<MenuItemDietaryModel> Create(MenuItemDietaryCreateModel model);
        Task Update(int id, MenuItemDietaryUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<MenuItemDietaryModel>> Search(PageSearchArgs args);
        Task<IPagedList<MenuItemDietaryModel>> Search(int menuItemId, int isActive, PageSearchArgs args);
    }
}
