using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemOptionService
    {
        Task<IEnumerable<MenuItemOptionModel>> Get(int menuItemId, int optionItemId);
        Task<MenuItemOptionModel> GetById(int id);
        Task<int> Create(MenuItemOptionModel model);
        Task<int> Update(int id, MenuItemOptionModel model);
        Task<bool> Delete(int id);
    }
}
