using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemSearchModel> GetDetails(int menuItemId);
        Task<List<int>> GetDietaries(int menuItemId);
        Task<List<int>> GetAllergens(int menuItemId);

        Task<IEnumerable<MenuItemModel>> Get(int menuId);
        Task<OrderItemModel> AddToCart(int menuId);
        Task<MenuItemModel> GetById(int id);
        Task<IEnumerable<MenuItemModel>> Get(List<int> allergenIds, List<int> dietaryIds);

        Task<MenuItemModel> Create(MenuItemCreateModel model);
        Task<MenuItemModel> Create(MenuItemDetailCreateModel model);
        Task Update(int id, MenuItemUpdateModel model);
        Task Update(int id, MenuItemDetailUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<MenuItemModel>> Search(PageSearchArgs args);
        Task<IPagedList<BranchResultModel>> Search(BranchSearchModel model, PageSearchArgs args);
        Task<IPagedList<MenuItemModel>> Search(int menuId, int isActive, PageSearchArgs args);
        Task<IPagedList<MenuItemModel>> SearchLazy(int menuId, int isActive, PageSearchArgs args);
    }
}
