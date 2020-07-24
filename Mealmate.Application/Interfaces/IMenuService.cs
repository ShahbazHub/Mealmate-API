using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuModel>> Get();
        Task<MenuModel> GetById(int id);
        Task<MenuModel> Create(MenuCreateModel model);
        Task Update(int id, MenuUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<MenuModel>> Search(PageSearchArgs args);
        Task<IPagedList<MenuModel>> Search(int branchId, int isActive, PageSearchArgs args);
    }
}
