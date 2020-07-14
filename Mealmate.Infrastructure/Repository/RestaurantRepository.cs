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
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(MealmateContext context)
            : base(context)
        {

        }

        public override async Task<Restaurant> GetByIdAsync(int id)
        {
            //TODO: should be refactored
            var Restaurants = await GetAsync(p => p.Id == id, null, new List<Expression<Func<Restaurant, object>>> { p => p.Branches });
            return Restaurants.FirstOrDefault();
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantListAsync()
        {
            var spec = new RestaurantWithBranchesSpecification();
            return await GetAsync(spec);

            // second way
            // return await GetAllAsync();
        }

        public Task<IPagedList<Restaurant>> SearchRestaurantsAsync(PageSearchArgs args)
        {
            var query = Table.Include(p => p.Branches);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Restaurant, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Restaurant, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Restaurant, object>>>(sortingOption, p => p.Name));
                            break;
                            //case "unitPrice":
                            //    orderByList.Add(new Tuple<SortingOption, Expression<Func<Restaurant, object>>>(sortingOption, p => p.UnitPrice));
                            //    break;
                            //case "category.name":
                            //    orderByList.Add(new Tuple<SortingOption, Expression<Func<Restaurant, object>>>(sortingOption, p => p.Category.Name));
                            //    break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Restaurant, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Restaurant, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Restaurant, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Restaurant, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                            //case "unitPrice":
                            //    filterList.Add(new Tuple<FilteringOption, Expression<Func<Restaurant, bool>>>(filteringOption, p => p.UnitPrice == (int)filteringOption.Value));
                            //    break;
                            //case "category.name":
                            //    filterList.Add(new Tuple<FilteringOption, Expression<Func<Restaurant, bool>>>(filteringOption, p => p.Category.Name.Contains((string)filteringOption.Value)));
                            //    break;
                    }
                }
            }

            var RestaurantPagedList = new PagedList<Restaurant>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Restaurant>>(RestaurantPagedList);
        }

        public async Task<IEnumerable<Restaurant>> GetRestaurantByNameAsync(string RestaurantName)
        {
            var spec = new RestaurantWithBranchesSpecification(RestaurantName);
            return await GetAsync(spec);

            // second way
            // return await GetAsync(x => x.RestaurantName.ToLower().Contains(RestaurantName.ToLower()));

            // third way
            //return await _dbContext.Restaurants
            //    .Where(x => x.RestaurantName.Contains(RestaurantName))
            //    .ToListAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdWithBranchesAsync(int RestaurantId)
        {
            var spec = new RestaurantWithBranchesSpecification(RestaurantId);
            return (await GetAsync(spec)).FirstOrDefault();
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurantWithBranchesByOwnerIdAsync(int OwnerId)
        {
            var spec = new RestaurantWithBranchesSpecification(p => p.OwnerId == OwnerId);
            return await GetAsync(spec);
        }
    }
}
