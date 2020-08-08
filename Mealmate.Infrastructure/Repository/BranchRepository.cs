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
    public class BranchRepository : Repository<Branch>, IBranchRepository
    {
        private readonly MealmateContext _context;
        public BranchRepository(MealmateContext context)
            : base(context)
        {
            _context = context;
        }
        public override async Task<Branch> GetByIdAsync(int id)
        {
            //_context.Branches.FirstOrDefaultAsync(p)
            //TODO: should be refactored
            var temp = await GetAsync(p => p.Id == id);
            return temp.FirstOrDefault();
        }

        public Task<IPagedList<Branch>> SearchAsync(int restaurantId, int isActive, PageSearchArgs args)
        {
            var query = Table.Include(p => p.Locations).Where(p => p.RestaurantId == restaurantId);
            if (isActive == 1 || isActive == 0)
            {
                var status = isActive == 1 ? true : false;
                query = query.Where(p => p.IsActive == status);
            }

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Branch, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Branch, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Branch, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Branch, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var RestaurantPagedList = new PagedList<Branch>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Branch>>(RestaurantPagedList);
        }

        public Task<IPagedList<Branch>> SearchAsync(PageSearchArgs args)
        {
            var query = Table.Include(p => p.Restaurant)
                             .Include(p => p.Locations)
                             .Include(p => p.Menus)
                             .ThenInclude(p => p.MenuItems)
                             .Where(p => p.IsActive == true);

            
            var orderByList = new List<Tuple<SortingOption, Expression<Func<Branch, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Branch, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Branch, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Branch, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Branch, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var RestaurantPagedList = new PagedList<Branch>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Branch>>(RestaurantPagedList);
        }


    }
}
