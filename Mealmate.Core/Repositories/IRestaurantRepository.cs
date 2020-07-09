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
        Task<IPagedList<Restaurant>> SearchRestaurantsAsync(PageSearchArgs args);
        Task<IEnumerable<Restaurant>> GetRestaurantByNameAsync(string RestaurantName);
        Task<Restaurant> GetRestaurantByIdWithBranchesAsync(int RestaurantId);
        //Task<IEnumerable<Restaurant>> GetRestaurantByBranchAsync(int branchId);
    }
}
