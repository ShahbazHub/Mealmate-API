using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetRestaurantListAsync();
        Task<IPagedList<Restaurant>> SearchAsync(PageSearchArgs args);
        Task<IEnumerable<Restaurant>> Get(string RestaurantName);
        Task<Restaurant> GetById(int RestaurantId);
        Task<IEnumerable<Restaurant>> GetByOwnerId(int OwnerId);
        //Task<IEnumerable<Restaurant>> GetRestaurantByBranchAsync(int branchId);
    }
}
