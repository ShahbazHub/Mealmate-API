using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserDietaryService
    {
        Task<IEnumerable<UserDietaryModel>> Get(int UserId);
        Task<UserDietaryModel> GetById(int id);
        Task<UserDietaryModel> Create(UserDietaryModel model);
        Task Update(UserDietaryModel model);
        Task Delete(int id);

        Task<IPagedList<UserDietaryModel>> Search(PageSearchArgs args);
        Task<IPagedList<UserDietaryModel>> Search(int menuItemId, PageSearchArgs args);
    }
}
