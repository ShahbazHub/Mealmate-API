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
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuItemAllergenRepository _menuItemAllergenRepository;
        private readonly IAppLogger<MenuItemService> _logger;
        private readonly IMapper _mapper;

        public MenuItemService(
            IMenuItemRepository menuItemRepository,
            IMenuItemAllergenRepository menuItemAllergenRepository,
            IAppLogger<MenuItemService> logger,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _menuItemAllergenRepository = menuItemAllergenRepository ?? throw new ArgumentNullException(nameof(menuItemAllergenRepository));
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
        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemList()
        //{
        //    var MenuItemList = await _menuItemRepository.ListAllAsync();

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<IPagedList<MenuItemModel>> SearchMenuItems(PageSearchArgs args)
        //{
        //    var MenuItemPagedList = await _menuItemRepository.SearchMenuItemsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var MenuItemModels = ObjectMapper.Mapper.Map<List<MenuItemModel>>(MenuItemPagedList.Items);

        //    var MenuItemModelPagedList = new PagedList<MenuItemModel>(
        //        MenuItemPagedList.PageIndex,
        //        MenuItemPagedList.PageSize,
        //        MenuItemPagedList.TotalCount,
        //        MenuItemPagedList.TotalPages,
        //        MenuItemModels);

        //    return MenuItemModelPagedList;
        //}

        //public async Task<MenuItemModel> GetMenuItemById(int MenuItemId)
        //{
        //    var MenuItem = await _menuItemRepository.GetByIdAsync(MenuItemId);

        //    var MenuItemModel = ObjectMapper.Mapper.Map<MenuItemModel>(MenuItem);

        //    return MenuItemModel;
        //}

        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemsByName(string name)
        //{
        //    var spec = new MenuItemWithMenuItemesSpecification(name);
        //    var MenuItemList = await _menuItemRepository.GetAsync(spec);

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<IEnumerable<MenuItemModel>> GetMenuItemsByCategoryId(int categoryId)
        //{
        //    var spec = new MenuItemWithMenuItemesSpecification(categoryId);
        //    var MenuItemList = await _menuItemRepository.GetAsync(spec);

        //    var MenuItemModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemModel>>(MenuItemList);

        //    return MenuItemModels;
        //}

        //public async Task<MenuItemModel> CreateMenuItem(MenuItemModel MenuItem)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItem.Id);
        //    if (existingMenuItem != null)
        //    {
        //        throw new ApplicationException("MenuItem with this id already exists");
        //    }

        //    var newMenuItem = ObjectMapper.Mapper.Map<MenuItem>(MenuItem);
        //    newMenuItem = await _menuItemRepository.SaveAsync(newMenuItem);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newMenuItemModel = ObjectMapper.Mapper.Map<MenuItemModel>(newMenuItem);
        //    return newMenuItemModel;
        //}

        //public async Task UpdateMenuItem(MenuItemModel MenuItem)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItem.Id);
        //    if (existingMenuItem == null)
        //    {
        //        throw new ApplicationException("MenuItem with this id is not exists");
        //    }

        //    existingMenuItem.Name = MenuItem.Name;
        //    existingMenuItem.Description = MenuItem.Description;

        //    await _menuItemRepository.SaveAsync(existingMenuItem);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteMenuItemById(int MenuItemId)
        //{
        //    var existingMenuItem = await _menuItemRepository.GetByIdAsync(MenuItemId);
        //    if (existingMenuItem == null)
        //    {
        //        throw new ApplicationException("MenuItem with this id is not exists");
        //    }

        //    await _menuItemRepository.DeleteAsync(existingMenuItem);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
