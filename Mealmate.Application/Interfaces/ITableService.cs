using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<TableModel>> Get(int locationId);
        Task<TableModel> GetById(int id);
        Task<TableModel> Create(TableModel model);
        Task Update(TableModel model);
        Task Delete(int id);
    }
}
