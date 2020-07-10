using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationModel>> Get(int branchId);
        Task<LocationModel> GetById(int id);
        Task<int> Create(LocationModel model);
        Task<int> Update(int id, LocationModel model);
        Task<bool> Delete(int id);
    }
}
