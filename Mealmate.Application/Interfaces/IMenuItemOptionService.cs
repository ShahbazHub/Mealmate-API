using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemOptionService
    {
        Task<IEnumerable<MenuItemOptionModel>> Get(int menuItemId, int optionItemId);
        Task<MenuItemOptionModel> GetById(int id);
        Task<MenuItemOptionModel> Create(MenuItemOptionModel model);
        Task Update(MenuItemOptionModel model);
        Task Delete(int id);
    }
}
