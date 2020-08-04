using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class ContactRequestRepository : Repository<ContactRequest>, IContactRequestRepository
    {
        public ContactRequestRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<ContactRequest>> SearchAsync(int customerId, PageSearchArgs args)
        {
            var query = Table.Where(p => p.CustomerId == customerId);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<ContactRequest, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<ContactRequest, object>>>(sortingOption, c => c.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<ContactRequest, object>>>(sortingOption, c => c.ContactRequestState.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<ContactRequest, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<ContactRequest, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<ContactRequest, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<ContactRequest, bool>>>(filteringOption, c => c.ContactRequestState.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<ContactRequest>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<ContactRequest>>(tempPagedList);
        }
    }
}
