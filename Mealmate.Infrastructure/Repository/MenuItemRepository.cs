using Mealmate.Core.Dtos;
using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly MealmateContext _context;
        public MenuItemRepository(MealmateContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetWithDetailsAsync()
        {
            var spec = new MenuItemSpecification();
            return await GetAsync(spec);
        }

        public Task<IPagedList<MenuItem>> SearchAsync(PageSearchArgs args)
        {
            var query = Table.Include(p => p.Menu)
                             .Include(p => p.MenuItemAllergens)
                             .Include(p => p.MenuItemDietaries)
                             .Include(p => p.MenuItemOptions);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<MenuItem, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<MenuItem>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<MenuItem>>(pagedList);
        }

        public Task<IPagedList<MenuItem>> SearchAsync(int menuId, int isActive, PageSearchArgs args)
        {
            var query = Table.Include(p => p.MenuItemAllergens)
                             .ThenInclude(u => u.Allergen)
                             .Include(p => p.MenuItemDietaries)
                             .ThenInclude(t => t.Dietary)
                             .Include(p => p.MenuItemOptions)
                             .Include(p => p.Menu)
                             .Where(p => p.MenuId == menuId);

            if (isActive == 1 || isActive == 0)
            {
                var status = isActive == 1 ? true : false;
                query = query.Where(p => p.IsActive == status);
            }

            var orderByList = new List<Tuple<SortingOption, Expression<Func<MenuItem, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuItem, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuItem, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<MenuItem>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<MenuItem>>(pagedList);
        }

        public Task<IPagedList<BranchResultDto>> SearchAsync(
            List<int> cuisineTypes,
            List<int> allergens,
            List<int> dietaries,
            PageSearchArgs args)
        {
            var query = Table.Include(p => p.Menu).ThenInclude(u => u.Branch).ThenInclude(u => u.Restaurant)
                            .Include(p => p.MenuItemDietaries)
                            .Include(p => p.MenuItemAllergens)
                            .Select(p => new BranchListDto
                            {
                                CuisineTypeId = p.CuisineTypeId,
                                Allergens = p.MenuItemAllergens.Select(t => t.AllergenId),
                                Dietaries = p.MenuItemDietaries.Select(t => t.DietaryId),
                                BranchId = p.Menu.Branch.Id,
                                Branch = p.Menu.Branch.Name,
                                Restaurant = p.Menu.Branch.Restaurant.Name,
                                Latitude = p.Menu.Branch.Latitude,
                                Longitude = p.Menu.Branch.Longitude,
                            }).ToList();

            // {
            //      BranchId
            //      Branch
            //      Restaurant
            //      Longitude
            //      Latitude
            //      TotalDishes
            //      FilteredDishes
            // }

            //1. Branch total dishes
            List<BranchResultDto> summary = query.GroupBy(p => p.BranchId)
                                            .Select(p => new BranchResultDto
                                            {
                                                BranchId = p.Key,
                                                TotalDishes = p.Count(),
                                                Restaurant = p.FirstOrDefault().Restaurant,
                                                Branch = p.FirstOrDefault().Branch,
                                                Latitude = p.FirstOrDefault().Latitude,
                                                Longitude = p.FirstOrDefault().Longitude,
                                                IsActive = true
                                            }).ToList();
            foreach (var item in summary)
            {
                Console.WriteLine($"Id {item.BranchId}, " +
                    $"Name: {item.Branch}, " +
                    $"Restaurant: {item.Restaurant} " +
                    $"Lat: {item.Latitude}, " +
                    $"Lon: {item.Longitude}, " +
                    $"Total: {item.TotalDishes}");
            }

            //2. Branch dishes which are having cuisineTypes[]
            if (cuisineTypes.Count > 0)
            {
                query = query.Where(p => cuisineTypes.Contains(p.CuisineTypeId)).ToList();
            }

            //3. Branch dishes which are not having allergens[]
            if (allergens.Count > 0 || dietaries.Count > 0)
            {
                foreach (var item in query)
                {
                    var menuItemAllergens = item.Allergens;
                    var menuItemDietaries = item.Dietaries;

                    // if menu item allergens contains any of the allergens passed as parameter
                    if (menuItemAllergens.Any(p => item.Allergens.Contains(p)))
                    {
                        item.IsActive = false;
                    }
                }
            }
            //4. Branch dishes which are having dietaries[]
            //5. Branch total filtered dishes

            var summaryFiltered = query.GroupBy(p => p.BranchId)
                                       .Select(p => new
                                       {
                                           BranchId = p.Key,
                                           FilteredDishes = p.Count(u => u.IsActive == true)
                                       }).ToList();

            for (int i = 0; i < summary.Count(); i++)
            {
                summary[i].FilteredDishes = summaryFiltered[i].FilteredDishes;
            }

            Console.WriteLine("After filtering");
            foreach (var item in summary)
            {
                Console.WriteLine($"Id {item.BranchId}, " +
                    $"Name: {item.Branch}, " +
                    $"Restaurant: {item.Restaurant} " +
                    $"Lat: {item.Latitude}, " +
                    $"Lon: {item.Longitude}, " +
                    $"Total: {item.TotalDishes}, " +
                    $"Filtered: {item.FilteredDishes}");
            }

            var orderByList = new List<Tuple<SortingOption, Expression<Func<BranchResultDto, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BranchResultDto, object>>>(sortingOption, p => p.BranchId));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BranchResultDto, object>>>(sortingOption, p => p.Branch));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<BranchResultDto, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.BranchId));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<BranchResultDto, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BranchResultDto, bool>>>(filteringOption, p => p.BranchId == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BranchResultDto, bool>>>(filteringOption, p => p.Branch.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<BranchResultDto>(
                summary.AsQueryable(),
                new PagingArgs
                {
                    PageIndex = args.PageIndex,
                    PageSize = args.PageSize,
                    PagingStrategy = args.PagingStrategy
                },
                orderByList,
                filterList);

            return Task.FromResult<IPagedList<BranchResultDto>>(pagedList);
        }
    }
}
