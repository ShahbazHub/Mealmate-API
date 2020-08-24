using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace Mealmate.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly MealmateContext _context;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuItemAllergenRepository _menuItemAllergenRepository;
        private readonly IMenuItemDietaryRepository _menuItemDietaryRepository;
        private readonly IAppLogger<MenuItemService> _logger;
        private readonly IMapper _mapper;

        public MenuItemService(
            IMenuItemRepository menuItemRepository,
            IMenuItemAllergenRepository menuItemAllergenRepository,
            IMenuItemDietaryRepository menuItemDietaryRepository,
            IAppLogger<MenuItemService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _menuItemAllergenRepository = menuItemAllergenRepository ?? throw new ArgumentNullException(nameof(menuItemAllergenRepository));
            _menuItemDietaryRepository = menuItemDietaryRepository ?? throw new ArgumentNullException(nameof(menuItemDietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuItemModel> Create(MenuItemCreateModel model)
        {
            var newmenuItem = new MenuItem
            {
                Created = DateTime.Now,
                CuisineTypeId = model.CuisineTypeId,
                Description = model.Description,
                IsActive = model.IsActive,
                MenuId = model.MenuId,
                Name = model.Name,
                Photo = model.Photo,
                Price = model.Price
            };

            newmenuItem = await _menuItemRepository.SaveAsync(newmenuItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemmodel = _mapper.Map<MenuItemModel>(newmenuItem);
            return newmenuItemmodel;
        }

        public async Task<MenuItemModel> Create(MenuItemDetailCreateModel model)
        {
            var newmenuItem = new MenuItem
            {
                Created = DateTime.Now,
                CuisineTypeId = model.CuisineTypeId,
                Description = model.Description,
                IsActive = model.IsActive,
                MenuId = model.MenuId,
                Name = model.Name,
                Photo = model.Photo,
                Price = model.Price
            };

            newmenuItem = await _menuItemRepository.SaveAsync(newmenuItem);
            if (newmenuItem != null)
            {
                foreach (var item in model.Allergens)
                {
                    var temp = new MenuItemAllergen
                    {
                        MenuItemId = newmenuItem.Id,
                        AllergenId = item.AllergenId,
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    await _menuItemAllergenRepository.SaveAsync(temp);
                }

                foreach (var item in model.Dietaries)
                {
                    var temp = new MenuItemDietary
                    {
                        MenuItemId = newmenuItem.Id,
                        DietaryId = item.DietaryId,
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    await _menuItemDietaryRepository.SaveAsync(temp);

                }
            }
            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemmodel = _mapper.Map<MenuItemModel>(newmenuItem);
            return newmenuItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            await _menuItemRepository.DeleteAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuItemModel>> Get(int menuId)
        {
            var result = await _menuItemRepository.GetAsync(x => x.MenuId == menuId);
            return _mapper.Map<IEnumerable<MenuItemModel>>(result);
        }

        public async Task<MenuItemModel> GetById(int id)
        {
            return _mapper.Map<MenuItemModel>(await _menuItemRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<MenuItemModel>> Get(List<int> allergenIds, List<int> dietaryIds)
        {
            var result = await _menuItemRepository.GetWithDetailsAsync();

            //TODO: filtering the menu items having allergens / dietaries

            var groupAllergens = result.GroupBy(p => p.MenuItemAllergens.Select(x => x.AllergenId));
            var groupDietaries = result.GroupBy(p => p.MenuItemDietaries.Select(x => x.DietaryId));

            return _mapper.Map<IEnumerable<MenuItemModel>>(result);
        }

        public async Task Update(int id, MenuItemUpdateModel model)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }


            existingMenuItem.Created = DateTime.Now;
            existingMenuItem.CuisineTypeId = model.CuisineTypeId;
            existingMenuItem.Description = model.Description;
            existingMenuItem.IsActive = model.IsActive;
            existingMenuItem.Name = model.Name;
            existingMenuItem.Photo = model.Photo;

            await _menuItemRepository.SaveAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task Update(int id, MenuItemDetailUpdateModel model)
        {
            var existingMenuItem = await _menuItemRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }


            existingMenuItem.Created = DateTime.Now;
            existingMenuItem.CuisineTypeId = model.CuisineTypeId;
            existingMenuItem.Description = model.Description;
            existingMenuItem.IsActive = model.IsActive;
            existingMenuItem.Name = model.Name;
            existingMenuItem.Photo = model.Photo;

            await _menuItemRepository.SaveAsync(existingMenuItem);

            if (model.Allergens.Count == 0)
            {
                var temp = await _menuItemAllergenRepository.GetAsync(p => p.MenuItemId == id);
                foreach (var item in temp)
                {
                    await _menuItemAllergenRepository.DeleteAsync(item);
                }
            }
            else
            {
                var temp1 = await _menuItemAllergenRepository.GetAsync(p => p.MenuItemId == id);
                foreach (var item in temp1)
                {
                    await _menuItemAllergenRepository.DeleteAsync(item);
                }

                foreach (var item in model.Allergens)
                {
                    if (item.MenuItemAllergenId != 0)
                    {
                        if (!item.IsActive)
                        {
                            var temp = await _menuItemAllergenRepository.GetByIdAsync(item.MenuItemAllergenId);

                            await _menuItemAllergenRepository.DeleteAsync(temp);
                        }
                    }
                    else
                    {
                        if (item.IsActive)
                        {
                            var temp = new MenuItemAllergen
                            {
                                MenuItemId = id,
                                AllergenId = item.AllergenId,
                                Created = DateTime.Now,
                                IsActive = true
                            };
                            await _menuItemAllergenRepository.SaveAsync(temp);
                        }
                        else
                        {
                            var temp = await _menuItemAllergenRepository.GetByIdAsync(item.MenuItemAllergenId);
                            if (temp != null)
                            {
                                await _menuItemAllergenRepository.DeleteAsync(temp);
                            }
                        }
                    }
                }
            }

            if (model.Dietaries.Count == 0)
            {
                var temp = await _menuItemDietaryRepository.GetAsync(p => p.MenuItemId == id);
                foreach (var item in temp)
                {
                    await _menuItemDietaryRepository.DeleteAsync(item);
                }
            }
            else
            {
                var temp1 = await _menuItemDietaryRepository.GetAsync(p => p.MenuItemId == id);
                foreach (var item in temp1)
                {
                    await _menuItemDietaryRepository.DeleteAsync(item);
                }

                foreach (var item in model.Dietaries)
                {
                    if (item.MenuItemDietaryId != 0)
                    {
                        if (!item.IsActive)
                        {
                            var temp = await _menuItemDietaryRepository.GetByIdAsync(item.MenuItemDietaryId);

                            await _menuItemDietaryRepository.DeleteAsync(temp);
                        }
                    }
                    else
                    {
                        if (item.IsActive)
                        {
                            var temp = new MenuItemDietary
                            {
                                MenuItemId = id,
                                DietaryId = item.DietaryId,
                                Created = DateTime.Now,
                                IsActive = true
                            };
                            await _menuItemDietaryRepository.SaveAsync(temp);
                        }
                        else
                        {
                            var temp = await _menuItemDietaryRepository.GetByIdAsync(item.MenuItemDietaryId);
                            if (temp != null)
                            {
                                await _menuItemDietaryRepository.DeleteAsync(temp);
                            }
                        }
                    }
                }
            }

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<MenuItemModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _menuItemRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }
        public async Task<IPagedList<BranchResultModel>> Search(BranchSearchModel model, PageSearchArgs args)
        {
            var TablePagedList = await _menuItemRepository.SearchAsync(model.CuisineTypes, model.Allergens, model.Dietaries, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<BranchResultModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<BranchResultModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<MenuItemModel>> Search(int menuId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _menuItemRepository.SearchAsync(menuId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<MenuItemSearchModel> GetDetails(int menuItemId)
        {
            var model = new MenuItemSearchModel();

            var allergens = await _context.MenuItemAllergens
                                    .Where(p => p.IsActive == true && p.MenuItemId == menuItemId)
                                    .Select(p => p.AllergenId)
                                    .ToListAsync();

            var dietaries = await _context.MenuItemDietaries
                                    .Where(p => p.IsActive == true && p.MenuItemId == menuItemId)
                                    .Select(p => p.DietaryId)
                                    .ToListAsync();

            model.Allergens = allergens;
            model.Dietaries = dietaries;
            return model;
        }

        public async Task<List<int>> GetDietaries(int menuItemId)
        {
            var dietaries = await _context.MenuItemDietaries
                                    .Where(p => p.IsActive == true && p.MenuItemId == menuItemId)
                                    .Select(p => p.DietaryId)
                                    .ToListAsync();

            return dietaries;
        }

        public async Task<List<int>> GetAllergens(int menuItemId)
        {
            var allergens = await _context.MenuItemAllergens
                                    .Where(p => p.IsActive == true && p.MenuItemId == menuItemId)
                                    .Select(p => p.AllergenId)
                                    .ToListAsync();

            return allergens;
        }

        public async Task<OrderItemModel> AddToCart(int menuId)
        {
            var orderItemModel = await _menuItemRepository
                                .Table
                                .Where(mi => mi.Id == menuId)
                                .Include(mi => mi.MenuItemOptions)
                                .ThenInclude(mio => mio.MenuItem)
                                .Select(mi => new OrderItemModel
                                {
                                    MenuItemId = mi.Id,
                                    MenuItemName = mi.Name,
                                    MenuItemDescription = mi.Description,
                                    Price = mi.Price,
                                    Quantity = 1,
                                    OrderItemDetails = mi.MenuItemOptions.Select(mio => new OrderItemDetailModel
                                    {
                                        MenuItemOptionName = mio.OptionItem.Name,
                                        MenuItemOptionId = mio.Id,
                                        Price = mio.Price,
                                        Quantity = mio.Quantity
                                    }).ToList()
                                })
                                .FirstOrDefaultAsync();
            return orderItemModel;
        }
    }
}
