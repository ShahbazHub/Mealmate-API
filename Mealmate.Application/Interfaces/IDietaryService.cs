using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IDietaryService
    {
        Task<IEnumerable<DietaryModel>> Get();
        Task<DietaryModel> GetById(int id);
        Task<DietaryModel> Create(DietaryModel model);
        Task Update(DietaryModel model);
        Task Delete(int id);
    }
}
