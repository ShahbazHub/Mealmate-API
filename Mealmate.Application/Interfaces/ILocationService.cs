using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationModel>> Get(int branchId);
        Task<LocationModel> GetById(int id);
        Task<LocationModel> Create(LocationModel model);
        Task Update(LocationModel model);
        Task Delete(int id);
    }
}
