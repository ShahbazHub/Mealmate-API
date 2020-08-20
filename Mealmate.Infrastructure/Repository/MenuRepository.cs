using Mealmate.Core.Dtos;
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
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<Menu>> SearchAsync(PageSearchArgs args)
        {
            var query = Table.Include(p => p.MenuItems);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Menu, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Menu, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Menu, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Menu, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<Menu>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Menu>>(pagedList);
        }

        public Task<IPagedList<Menu>> SearchAsync(int branchId, int isActive, PageSearchArgs args)
        {
            var query = Table.Include(p => p.MenuItems).ThenInclude(p => p.MenuItemAllergens)
                            .Include(p => p.MenuItems).ThenInclude(p => p.MenuItemDietaries)
                            .Where(p => p.BranchId == branchId);
            if (isActive == 1 || isActive == 0)
            {
                var status = isActive == 1 ? true : false;
                query = query.Where(p => p.IsActive == status);
            }

            var orderByList = new List<Tuple<SortingOption, Expression<Func<Menu, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(sortingOption, p => p.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<Menu, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<Menu, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Menu, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<Menu, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<Menu>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<Menu>>(pagedList);
        }


        public Task<IPagedList<MenuDto>> SearchAsync(int branchId, List<int> allergens, List<int> dietaries, PageSearchArgs args)
        {
            try
            {
                var query = Table.Include(p => p.MenuItems).ThenInclude(p => p.MenuItemAllergens)
                             .Include(p => p.MenuItems).ThenInclude(p => p.MenuItemDietaries)
                            .Where(p => p.BranchId == branchId)
                            .Where(p => p.IsActive == true);
                List<MenuDto> menus = new List<MenuDto>();
                foreach (var menu in query)
                {
                    var temp = new MenuDto()
                    {
                        Id = menu.Id,
                        Name = menu.Name,
                        ServiceTime = menu.ServiceTime,
                        MenuItems = menu.MenuItems
                                    .Where(x => x.IsActive == true)
                                    .Select(x => new MenuItemDto
                                    {
                                        Id = x.Id,
                                        Name = x.Name,
                                        Photo = x.Photo,
                                        Price = x.Price
                                    }).ToList()
                    };

                    foreach (var menuItem in menu.MenuItems)
                    {
                        var menuItemAllergens = menuItem.MenuItemAllergens.Select(p => p.AllergenId);
                        var menuItemDietaries = menuItem.MenuItemDietaries.Select(t => t.DietaryId);

                        var allergenResult = menuItemAllergens.Intersect(allergens);
                        if (allergenResult.Count() == 0)
                        {
                            var dietaryResult = menuItemDietaries.Intersect(dietaries);
                            if (dietaryResult.Count() > 0)
                            {
                                var menuItemTemp = new MenuItemDto
                                {
                                    Id = menuItem.Id,
                                    Description = menuItem.Description,
                                    Name = menuItem.Name,
                                    Photo = menuItem.Photo,
                                    Price = menuItem.Price
                                };

                                temp.MenuItems.Add(menuItemTemp);
                            }
                        }
                    }

                    menus.Add(temp);
                }

                var orderByList = new List<Tuple<SortingOption, Expression<Func<MenuDto, object>>>>();

                if (args.SortingOptions != null)
                {
                    foreach (var sortingOption in args.SortingOptions)
                    {
                        switch (sortingOption.Field)
                        {
                            case "id":
                                orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuDto, object>>>(sortingOption, p => p.Id));
                                break;
                            case "name":
                                orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuDto, object>>>(sortingOption, p => p.Name));
                                break;
                        }
                    }
                }

                if (orderByList.Count == 0)
                {
                    orderByList.Add(new Tuple<SortingOption, Expression<Func<MenuDto, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
                }

                //TODO: FilteringOption.Operator will be handled
                var filterList = new List<Tuple<FilteringOption, Expression<Func<MenuDto, bool>>>>();

                if (args.FilteringOptions != null)
                {
                    foreach (var filteringOption in args.FilteringOptions)
                    {
                        switch (filteringOption.Field)
                        {
                            case "id":
                                filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuDto, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                                break;
                            case "name":
                                filterList.Add(new Tuple<FilteringOption, Expression<Func<MenuDto, bool>>>(filteringOption, p => p.Name.Contains((string)filteringOption.Value)));
                                break;
                        }
                    }
                }

                var pagedList = new PagedList<MenuDto>(menus.AsQueryable<MenuDto>(),
                    new PagingArgs
                    {
                        PageIndex = args.PageIndex,
                        PageSize = args.PageSize,
                        PagingStrategy = args.PagingStrategy
                    }, orderByList, filterList);

                return Task.FromResult<IPagedList<MenuDto>>(pagedList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
