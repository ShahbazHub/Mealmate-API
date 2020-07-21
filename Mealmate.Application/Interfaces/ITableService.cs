using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ITableService
    {
        Task<IEnumerable<TableModel>> Get(int locationId);
        Task<TableModel> GetById(int id);
        Task<TableModel> Create(TableCreateModel model);
        Task Update(int id, TableUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<TableModel>> Search(PageSearchArgs args);
        Task<IPagedList<TableModel>> Search(int locationId, int isActive, PageSearchArgs args);
    }
}
