using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuModel>> Get(int branchId);
        Task<MenuModel> GetById(int id);
        Task<int> Create(MenuModel model);
        Task<int> Update(int id, MenuModel model);
        Task<bool> Delete(int id);
    }
}
