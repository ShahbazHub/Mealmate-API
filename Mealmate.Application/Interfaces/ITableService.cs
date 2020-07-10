using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<TableModel>> Get(int locationId);
        Task<TableModel> GetById(int id);
        Task<int> Create(TableModel model);
        Task<int> Update(int id, TableModel model);
        Task<bool> Delete(int id);
    }
}
