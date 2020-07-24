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
        Task<UserDietaryModel> Create(UserDietaryCreateModel model);
        Task Update(int id, UserDietaryUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<UserDietaryModel>> Search(PageSearchArgs args);
        Task<IPagedList<UserDietaryModel>> Search(int userId, int isActive, PageSearchArgs args);
    }
}
