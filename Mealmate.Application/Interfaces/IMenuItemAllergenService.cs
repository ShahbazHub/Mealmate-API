using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemAllergenService
    {
        Task<IEnumerable<MenuItemAllergenModel>> Get(int menuItemId);
        Task<MenuItemAllergenModel> GetById(int id);
        Task<MenuItemAllergenModel> Create(MenuItemAllergenModel model);
        Task Update(MenuItemAllergenModel model);
        Task Delete(int id);
    }
}
