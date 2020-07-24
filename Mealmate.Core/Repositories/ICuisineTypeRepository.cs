using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface ICuisineTypeRepository : IRepository<CuisineType>
    {
        Task<IPagedList<CuisineType>> SearchAsync(int isActive, PageSearchArgs args);
    }
}
