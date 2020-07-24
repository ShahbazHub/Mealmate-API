using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserRestaurantService
    {
        Task<IEnumerable<UserRestaurantModel>> Get(int UserId);
        Task<UserRestaurantModel> GetById(int id);
        Task<UserRestaurantModel> Create(UserRestaurantCreateModel model);
        Task Update(int id, UserRestaurantUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<UserRestaurantModel>> Search(PageSearchArgs args);
        Task<IPagedList<UserRestaurantModel>> Search(int ownerId, PageSearchArgs args);
    }
}
