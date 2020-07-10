using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemModel>> Get(int menuId);
        Task<MenuItemModel> GetById(int id);
        Task<int> Create(MenuItemModel model);
        Task<int> Update(int id, MenuItemModel model);
        Task<bool> Delete(int id);
    }
}
