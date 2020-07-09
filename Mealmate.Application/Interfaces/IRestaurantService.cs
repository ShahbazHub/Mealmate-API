using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantModel>> GetRestaurantList();
        Task<IPagedList<RestaurantModel>> SearchRestaurants(PageSearchArgs args);
        Task<RestaurantModel> GetRestaurantById(int RestaurantId);
        Task<IEnumerable<RestaurantModel>> GetRestaurantsByName(string name);
        Task<IEnumerable<RestaurantModel>> GetRestaurantsByCategoryId(int categoryId);
        Task<RestaurantModel> CreateRestaurant(RestaurantModel Restaurant);
        Task UpdateRestaurant(RestaurantModel Restaurant);
        Task DeleteRestaurantById(int RestaurantId);
    }
}
