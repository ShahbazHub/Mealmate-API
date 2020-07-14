using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemDietaryService
    {
        Task<IEnumerable<MenuItemDietaryModel>> Get(int menuItemId);
        Task<MenuItemDietaryModel> GetById(int id);
        Task<MenuItemDietaryModel> Create(MenuItemDietaryModel model);
        Task Update(MenuItemDietaryModel model);
        Task Delete(int id);
    }
}
