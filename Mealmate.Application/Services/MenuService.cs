using System;
using System.Collections.Generic;
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
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IAppLogger<MenuService> _logger;
        private readonly IMapper _mapper;

        public MenuService(
            IMenuRepository menuRepository,
            IAppLogger<MenuService> logger,
            IMapper mapper)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuModel> Create(MenuModel model)
        {
            var newmenu = _mapper.Map<Menu>(model);
            newmenu = await _menuRepository.SaveAsync(newmenu);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenumodel = _mapper.Map<MenuModel>(newmenu);
            return newmenumodel;
        }

        public async Task Delete(int id)
        {
            var existingMenu = await _menuRepository.GetByIdAsync(id);
            if (existingMenu == null)
            {
                throw new ApplicationException("Menu with this id is not exists");
            }

            await _menuRepository.DeleteAsync(existingMenu);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuModel>> Get()
        {
            var result = await _menuRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<MenuModel>>(result);
        }

        public async Task<MenuModel> GetById(int id)
        {
            return _mapper.Map<MenuModel>(await _menuRepository.GetByIdAsync(id));
        }

        public async Task Update(MenuModel model)
        {
            var existingMenu = await _menuRepository.GetByIdAsync(model.Id);
            if (existingMenu == null)
            {
                throw new ApplicationException("Menu with this id is not exists");
            }

            existingMenu = _mapper.Map<Menu>(model);

            await _menuRepository.SaveAsync(existingMenu);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<MenuModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _menuRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<MenuModel>> Search(int branchId, PageSearchArgs args)
        {
            var TablePagedList = await _menuRepository.SearchAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        //public async Task<IEnumerable<MenuModel>> GetMenuList()
        //{
        //    var MenuList = await _menuRepository.ListAllAsync();

        //    var MenuModels = ObjectMapper.Mapper.Map<IEnumerable<MenuModel>>(MenuList);

        //    return MenuModels;
        //}

        //public async Task<IPagedList<MenuModel>> SearchMenus(PageSearchArgs args)
        //{
        //    var MenuPagedList = await _menuRepository.SearchMenusAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var MenuModels = ObjectMapper.Mapper.Map<List<MenuModel>>(MenuPagedList.Items);

        //    var MenuModelPagedList = new PagedList<MenuModel>(
        //        MenuPagedList.PageIndex,
        //        MenuPagedList.PageSize,
        //        MenuPagedList.TotalCount,
        //        MenuPagedList.TotalPages,
        //        MenuModels);

        //    return MenuModelPagedList;
        //}

        //public async Task<MenuModel> GetMenuById(int MenuId)
        //{
        //    var Menu = await _menuRepository.GetByIdAsync(MenuId);

        //    var MenuModel = ObjectMapper.Mapper.Map<MenuModel>(Menu);

        //    return MenuModel;
        //}

        //public async Task<IEnumerable<MenuModel>> GetMenusByName(string name)
        //{
        //    var spec = new MenuWithMenuesSpecification(name);
        //    var MenuList = await _menuRepository.GetAsync(spec);

        //    var MenuModels = ObjectMapper.Mapper.Map<IEnumerable<MenuModel>>(MenuList);

        //    return MenuModels;
        //}

        //public async Task<IEnumerable<MenuModel>> GetMenusByCategoryId(int categoryId)
        //{
        //    var spec = new MenuWithMenuesSpecification(categoryId);
        //    var MenuList = await _menuRepository.GetAsync(spec);

        //    var MenuModels = ObjectMapper.Mapper.Map<IEnumerable<MenuModel>>(MenuList);

        //    return MenuModels;
        //}

        //public async Task<MenuModel> CreateMenu(MenuModel Menu)
        //{
        //    var existingMenu = await _menuRepository.GetByIdAsync(Menu.Id);
        //    if (existingMenu != null)
        //    {
        //        throw new ApplicationException("Menu with this id already exists");
        //    }

        //    var newMenu = ObjectMapper.Mapper.Map<Menu>(Menu);
        //    newMenu = await _menuRepository.SaveAsync(newMenu);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newMenuModel = ObjectMapper.Mapper.Map<MenuModel>(newMenu);
        //    return newMenuModel;
        //}

        //public async Task UpdateMenu(MenuModel Menu)
        //{
        //    var existingMenu = await _menuRepository.GetByIdAsync(Menu.Id);
        //    if (existingMenu == null)
        //    {
        //        throw new ApplicationException("Menu with this id is not exists");
        //    }

        //    existingMenu.Name = Menu.Name;
        //    existingMenu.Description = Menu.Description;

        //    await _menuRepository.SaveAsync(existingMenu);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteMenuById(int MenuId)
        //{
        //    var existingMenu = await _menuRepository.GetByIdAsync(MenuId);
        //    if (existingMenu == null)
        //    {
        //        throw new ApplicationException("Menu with this id is not exists");
        //    }

        //    await _menuRepository.DeleteAsync(existingMenu);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
