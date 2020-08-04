using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IContactRequestStateService
    {
        Task<IEnumerable<ContactRequestStateModel>> Get();
        Task<ContactRequestStateModel> GetById(int id);
        Task<ContactRequestStateModel> Create(ContactRequestStateCreateModel model);
        Task Update(int id, ContactRequestStateUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<ContactRequestStateModel>> Search(int isActive, PageSearchArgs args);
    }
}
