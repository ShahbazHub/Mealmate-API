using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemModel>> Get(int menuId);
        Task<MenuItemModel> GetById(int id);
        Task<IEnumerable<MenuItemModel>> Get(List<int> allergenIds, List<int> dietaryIds);

        Task<MenuItemModel> Create(MenuItemModel model);
        Task Update(MenuItemModel model);
        Task Delete(int id);
    }
}
