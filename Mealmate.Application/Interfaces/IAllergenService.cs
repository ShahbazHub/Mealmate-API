using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IAllergenService
    {
        Task<IEnumerable<AllergenModel>> Get();
        Task<AllergenModel> GetById(int id);
        Task<AllergenModel> Create(AllergenCreateModel model);
        Task Update(int id, AllergenUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<AllergenModel>> Search(int isActive, PageSearchArgs args);
    }
}
