using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IUserDietaryRepository : IRepository<UserDietary>
    {
        Task<IPagedList<UserDietary>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<UserDietary>> SearchAsync(int userId, int isActive, PageSearchArgs args);
    }
}
