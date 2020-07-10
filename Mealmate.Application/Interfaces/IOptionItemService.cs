using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOptionItemService
    {
        Task<IEnumerable<OptionItemModel>> Get(int branchId);
        Task<OptionItemModel> GetById(int id);
        Task<int> Create(OptionItemModel model);
        Task<int> Update(int id, OptionItemModel model);
        Task<bool> Delete(int id);
    }
}
