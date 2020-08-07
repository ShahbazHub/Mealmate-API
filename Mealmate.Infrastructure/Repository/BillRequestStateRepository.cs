using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class BillRequestStateRepository : Repository<BillRequestState>, IBillRequestStateRepository
    {
        public BillRequestStateRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<BillRequestState>> SearchAsync(int isActive, PageSearchArgs args)
        {
            var query = Table;
            if (isActive == 1 || isActive == 0)
            {
                var status = isActive == 1;
                query = query.Where(p => p.IsActive == status);
            }

            var orderByList = new List<Tuple<SortingOption, Expression<Func<BillRequestState, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequestState, object>>>(sortingOption, c => c.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequestState, object>>>(sortingOption, c => c.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequestState, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<BillRequestState, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BillRequestState, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BillRequestState, bool>>>(filteringOption, c => c.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<BillRequestState>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<BillRequestState>>(tempPagedList);
        }
    }
}
