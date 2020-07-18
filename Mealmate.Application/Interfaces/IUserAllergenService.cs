using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserAllergenService
    {
        Task<IEnumerable<UserAllergenModel>> Get(int UserId);
        Task<UserAllergenModel> GetById(int id);
        Task<UserAllergenModel> Create(UserAllergenModel model);
        Task Update(UserAllergenModel model);
        Task Delete(int id);

        Task<IPagedList<UserAllergenModel>> Search(PageSearchArgs args);
        Task<IPagedList<UserAllergenModel>> Search(int menuItemId, PageSearchArgs args);
    }
}
