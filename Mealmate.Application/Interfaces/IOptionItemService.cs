using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOptionItemService
    {
        Task<IEnumerable<OptionItemModel>> Get();
        Task<OptionItemModel> GetById(int id);
        Task<OptionItemModel> Create(OptionItemModel model);
        Task Update(OptionItemModel model);
        Task Delete(int id);
    }
}
