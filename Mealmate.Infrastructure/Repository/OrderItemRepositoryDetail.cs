﻿using Mealmate.Core.Entities;
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
    public class OrderItemDetailRepository : Repository<OrderItemDetail>, IOrderItemDetailRepository
    {
        public OrderItemDetailRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<OrderItemDetail>> SearchAsync(PageSearchArgs args)
        {
            var query = Table;

            var orderByList = new List<Tuple<SortingOption, Expression<Func<OrderItemDetail, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<OrderItemDetail, object>>>(sortingOption, c => c.Id));
                            break;
                        case "price":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<OrderItemDetail, object>>>(sortingOption, c => c.Price));
                            break;
                        case "quantity":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<OrderItemDetail, object>>>(sortingOption, c => c.Quantity));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<OrderItemDetail, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<OrderItemDetail, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<OrderItemDetail, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "price":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<OrderItemDetail, bool>>>(filteringOption, c => c.Price == (decimal)filteringOption.Value));
                            break;
                        case "quantity":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<OrderItemDetail, bool>>>(filteringOption, c => c.Quantity == (int)filteringOption.Value));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<OrderItemDetail>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<OrderItemDetail>>(tempPagedList);
        }

    }
}
