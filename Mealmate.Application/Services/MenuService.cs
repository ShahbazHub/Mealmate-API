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

        public async Task<MenuModel> Create(MenuCreateModel model)
        {
            var newmenu = new Menu
            {
                BranchId = model.BranchId,
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name,
                ServiceTime = model.ServiceTime
            };

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
            var result = await _menuRepository.GetByIdAsync(id);
            return _mapper.Map<MenuModel>(result);
        }

        public async Task Update(int id, MenuUpdateModel model)
        {
            var existingMenu = await _menuRepository.GetByIdAsync(id);
            if (existingMenu == null)
            {
                throw new ApplicationException("Menu with this id is not exists");
            }

            existingMenu.IsActive = model.IsActive;
            existingMenu.Name = model.Name;
            existingMenu.ServiceTime = model.ServiceTime;

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

        public async Task<IPagedList<MenuListModel>> Search(int branchId, MenuItemSearchModel model, PageSearchArgs args)
        {
            try
            {
                var TablePagedList = await _menuRepository.SearchAsync(branchId, model.Allergens, model.Dietaries, args);

                //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
                var AllergenModels = _mapper.Map<List<MenuListModel>>(TablePagedList.Items);

                var AllergenModelPagedList = new PagedList<MenuListModel>(
                    TablePagedList.PageIndex,
                    TablePagedList.PageSize,
                    TablePagedList.TotalCount,
                    TablePagedList.TotalPages,
                    AllergenModels);

                return AllergenModelPagedList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IPagedList<MenuModel>> Search(int branchId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _menuRepository.SearchAsync(branchId, isActive, args);

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
    }
}
