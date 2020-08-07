using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IContactRequestService
    {
        Task<IEnumerable<ContactRequestModel>> Get();
        Task<IEnumerable<ContactRequestModel>> Get(int branchId, int contactRequestStateId);

        Task<ContactRequestModel> GetById(int id);
        Task<ContactRequestModel> Create(ContactRequestCreateModel model);
        Task Update(int id, ContactRequestUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<ContactRequestModel>> Search(int branchId, PageSearchArgs args);
    }
}
