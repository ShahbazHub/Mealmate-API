using Mealmate.Core.Entities;
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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<Order>> SearchAsync(PageSearchArgs args)
        {
            var query = Table;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Order, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Order, object>>>(sortingOption, c => c.Id));
                            break;
                        case "ordernumber":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Order, object>>>(sortingOption, c => c.OrderNumber));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Order, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<Order, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Order, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "ordernumber":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Order, bool>>>(filteringOption, c => c.OrderNumber.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<Order>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Order>>(tempPagedList);
        }

    }
}
