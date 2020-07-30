using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IUserRestaurantRepository : IRepository<UserRestaurant>
    {
        Task<IPagedList<UserRestaurant>> SearchAsync(PageSearchArgs args);
        Task<IEnumerable<UserRestaurant>> Search(int ownerId);
        Task<IPagedList<UserRestaurant>> SearchAsync(int ownerId, PageSearchArgs args);
        Task<IPagedList<UserRestaurant>> ListAsync(int restaurantId, PageSearchArgs args);
    }
}
