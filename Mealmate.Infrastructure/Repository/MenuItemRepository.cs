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
        public MenuItemRepository(MealmateContext context)
            : base(context)
        {
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

        public Task<IPagedList<BranchResultDto>> SearchAsync(List<int> cuisineTypes, List<int> allergens, List<int> dietaries, PageSearchArgs args)
        {
            //var query1 = Table.Include(p => p.Menu)
            //                .ThenInclude(u => u.Branch)
            //                .Include(p => p.MenuItemDietaries)
            //                .Include(p => p.MenuItemAllergens)
            //                .Where(p=>p.Menu.BranchId == 1)
            //                .Select(p => new BranchListDto
            //                {
            //                    CuisineTypeId = p.CuisineTypeId,
            //                    Allergens = p.MenuItemAllergens.Select(t => t.AllergenId),
            //                    Dietaries = p.MenuItemDietaries.Select(t => t.DietaryId),
            //                    BranchId = p.Menu.Branch.Id,
            //                    Branch = p.Menu.Branch.Name
            //                }).ToList();

            var query = Table.Include(p => p.Menu)
                             .ThenInclude(u => u.Branch)
                             .Include(ma => ma.MenuItemAllergens)
                             .Include(p => p.MenuItemDietaries)
                             .Select(p => new BranchListDto
                             {
                                 CuisineTypeId = p.CuisineTypeId,
                                 BranchId = p.Menu.Branch.Id,
                                 Branch = p.Menu.Branch.Name,
                                 Allergens = p.MenuItemAllergens.Select(t => t.AllergenId),
                                 Dietaries = p.MenuItemDietaries.Select(t => t.DietaryId)
                             }).ToList();

            var resultGroup = query.GroupBy(p => p.BranchId).Select(p => new
            {
                BranchId = p.Key,
                Total = p.Count(),
                Filtered = p.ToList().Where(d => cuisineTypes.Contains(d.CuisineTypeId)).Count(),
                Allergens = p.ToList().Select(d => d.Allergens),
                Dietaries = p.ToList().Select(d => d.Dietaries),
            });

            foreach (var item in resultGroup)
            {
                Console.WriteLine($"{item}");
            }

            //var resultBranch = query.GroupBy(p => p.BranchId);
            List<BranchResultDto> result = new List<BranchResultDto>();

            //foreach (var itemBranch in resultBranch)
            //{
            //    var entry = new BranchResultDto
            //    {
            //        BranchId = itemBranch.Key,
            //        //BranchId = itemBranch.Key.BranchId,
            //        //Branch = itemBranch.Key.Branch,
            //        //Latitude = itemBranch.Key.Latitude,
            //        //Longitude = itemBranch.Key.Longitude,
            //        //TotalDishes = itemBranch.Value.Count
            //    };

            //    //var filtered = itemBranch.Value.Where(p => cuisineTypes.Contains(p.CuisineTypeId));
            //    //entry.FilteredDishes = filtered.Count();
            //    foreach (var item in itemBranch)
            //    {
            //        IEnumerable<int> allergensTemp = item.Allergens;
            //        IEnumerable<int> dietariesTemp = item.Dietaries;

            //        if (allergens.Count > 0 && dietaries.Count > 0)
            //        {
            //            var resultAllergens = allergensTemp.Intersect(allergens);
            //            if (resultAllergens.Count() == 0)
            //            {
            //                var resultDietaries = dietariesTemp.Intersect(dietaries);
            //                if (resultDietaries.Count() > 0)
            //                {
            //                    entry.FilteredDishes += 1;
            //                    result.Add(entry);
            //                }
            //            }
            //        }
            //        else if (allergens.Count > 0 && dietaries.Count == 0)
            //        {
            //            var resultAllergens = allergensTemp.Intersect(allergens);
            //            if (resultAllergens.Count() == 0)
            //            {
            //                entry.FilteredDishes += 1;
            //                result.Add(entry);
            //            }
            //        }
            //        else if (allergens.Count == 0 && dietaries.Count > 0)
            //        {
            //            var resultDietaries = dietariesTemp.Intersect(dietaries);
            //            if (resultDietaries.Count() > 0)
            //            {
            //                entry.FilteredDishes += 1;
            //            }
            //        }
            //        else if (allergens.Count == 0 && dietaries.Count == 0)
            //        {
            //            entry.FilteredDishes += 1;
            //            result.Add(entry);
            //        }
            //    }
            //}

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
                result.AsQueryable(),
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
