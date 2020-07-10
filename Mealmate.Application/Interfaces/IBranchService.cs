using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchModel>> Get(int restaurantId);
        Task<BranchModel> GetById(int id);
        Task<int> Create(BranchModel model);
        Task<int> Update(int id, BranchModel model);
        Task<bool> Delete(int id);
    }
}
