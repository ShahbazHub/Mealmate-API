using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ICuisineTypeService
    {
        Task<IEnumerable<CuisineTypeModel>> Get();
        Task<CuisineTypeModel> GetById(int id);
        Task<CuisineTypeModel> Create(CuisineTypeCreateModel model);
        Task Update(int id, CuisineTypeUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<CuisineTypeModel>> Search(int isActive, PageSearchArgs args);
    }
}
