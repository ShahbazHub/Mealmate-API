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
    public class RestroomRequestRepository : Repository<RestroomRequest>, IRestroomRequestRepository
    {
        public RestroomRequestRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<RestroomRequest>> SearchAsync(int restaurantId, PageSearchArgs args)
        {
            var query = Table
                            .Include(u => u.Customer)
                            .Include(p => p.Table)
                            .ThenInclude(l => l.Location)
                            .ThenInclude(b => b.Branch)
                            .ThenInclude(r => r.Restaurant)
                            .Where(p => p.Table.Location.Branch.RestaurantId == restaurantId)
                            .OrderByDescending(p => p.RequestTime);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<RestroomRequest, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<RestroomRequest, object>>>(sortingOption, c => c.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<RestroomRequest, object>>>(sortingOption, c => c.RestRoomRequestState.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<RestroomRequest, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<RestroomRequest, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<RestroomRequest, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<RestroomRequest, bool>>>(filteringOption, c => c.RestRoomRequestState.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<RestroomRequest>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<RestroomRequest>>(tempPagedList);
        }
    }
}
