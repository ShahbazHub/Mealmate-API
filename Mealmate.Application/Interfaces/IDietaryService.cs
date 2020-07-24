using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IDietaryService
    {
        Task<IEnumerable<DietaryModel>> Get();
        Task<DietaryModel> GetById(int id);
        Task<DietaryModel> Create(DietaryCreateModel model);
        Task Update(int id, DietaryUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<DietaryModel>> Search(int isActive, PageSearchArgs args);
    }
}
