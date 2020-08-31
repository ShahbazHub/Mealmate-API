using Mealmate.Core.Dtos;
using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetWithDetailsAsync();
        Task<IPagedList<BranchResultDto>> SearchAsync(List<int> cuisineTypes, List<int> allergens, List<int> dietaries, PageSearchArgs args);
        Task<IPagedList<MenuItem>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<MenuItem>> SearchAsync(int menuId, int isActive, PageSearchArgs args);
        Task<IPagedList<MenuItem>> SearchLazyAsync(int menuId, int isActive, PageSearchArgs args);
    }
}
