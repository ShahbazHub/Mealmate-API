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
    public class MenuItemOptionService : IMenuItemOptionService
    {
        private readonly IMenuItemOptionRepository _menuItemOptionRepository;
        private readonly IAppLogger<MenuItemOptionService> _logger;
        private readonly IMapper _mapper;

        public MenuItemOptionService(
            IMenuItemOptionRepository menuItemOptionRepository,
            IAppLogger<MenuItemOptionService> logger,
            IMapper mapper)
        {
            _menuItemOptionRepository = menuItemOptionRepository ?? throw new ArgumentNullException(nameof(menuItemOptionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuItemOptionModel> Create(MenuItemOptionModel model)
        {
            var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(model.Id);
            if (existingMenuItemOption != null)
            {
                throw new ApplicationException("menuItemOption with this id already exists");
            }

            var newmenuItemOption = _mapper.Map<MenuItemOption>(model);
            newmenuItemOption = await _menuItemOptionRepository.SaveAsync(newmenuItemOption);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemOptionmodel = _mapper.Map<MenuItemOptionModel>(newmenuItemOption);
            return newmenuItemOptionmodel;
        }

        public async Task Delete(int id)
        {
            var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(id);
            if (existingMenuItemOption == null)
            {
                throw new ApplicationException("MenuItemOption with this id is not exists");
            }

            await _menuItemOptionRepository.DeleteAsync(existingMenuItemOption);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuItemOptionModel>> Get(int menuItemId, int optionItemId)
        {
            var result = await _menuItemOptionRepository.GetAsync(x => x.MenuItemId == menuItemId && x.OptionItemId == optionItemId);
            return _mapper.Map<IEnumerable<MenuItemOptionModel>>(result);
        }

        public async Task<MenuItemOptionModel> GetById(int id)
        {
            return _mapper.Map<MenuItemOptionModel>(await _menuItemOptionRepository.GetByIdAsync(id));
        }

        public async Task Update(MenuItemOptionModel model)
        {
            var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(model.Id);
            if (existingMenuItemOption == null)
            {
                throw new ApplicationException("MenuItemOption with this id is not exists");
            }

            existingMenuItemOption = _mapper.Map<MenuItemOption>(model);

            await _menuItemOptionRepository.SaveAsync(existingMenuItemOption);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<MenuItemOption>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _menuItemOptionRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemOption>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemOption>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<MenuItemOption>> Search(int branchId, PageSearchArgs args)
        {
            var TablePagedList = await _menuItemOptionRepository.SearchAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemOption>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemOption>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        Task<IPagedList<MenuItemOptionModel>> IMenuItemOptionService.Search(PageSearchArgs args)
        {
            throw new NotImplementedException();
        }

        Task<IPagedList<MenuItemOptionModel>> IMenuItemOptionService.Search(int menuItemId, PageSearchArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
