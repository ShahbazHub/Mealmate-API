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
            var result = await _menuItemOptionRepository.GetAsync(x => x.MenuItemId== menuItemId && x.OptionItemId == optionItemId);
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

        //public async Task<IEnumerable<MenuItemOptionModel>> GetMenuItemOptionList()
        //{
        //    var MenuItemOptionList = await _menuItemOptionRepository.ListAllAsync();

        //    var MenuItemOptionModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemOptionModel>>(MenuItemOptionList);

        //    return MenuItemOptionModels;
        //}

        //public async Task<IPagedList<MenuItemOptionModel>> SearchMenuItemOptions(PageSearchArgs args)
        //{
        //    var MenuItemOptionPagedList = await _menuItemOptionRepository.SearchMenuItemOptionsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var MenuItemOptionModels = ObjectMapper.Mapper.Map<List<MenuItemOptionModel>>(MenuItemOptionPagedList.Items);

        //    var MenuItemOptionModelPagedList = new PagedList<MenuItemOptionModel>(
        //        MenuItemOptionPagedList.PageIndex,
        //        MenuItemOptionPagedList.PageSize,
        //        MenuItemOptionPagedList.TotalCount,
        //        MenuItemOptionPagedList.TotalPages,
        //        MenuItemOptionModels);

        //    return MenuItemOptionModelPagedList;
        //}

        //public async Task<MenuItemOptionModel> GetMenuItemOptionById(int MenuItemOptionId)
        //{
        //    var MenuItemOption = await _menuItemOptionRepository.GetByIdAsync(MenuItemOptionId);

        //    var MenuItemOptionModel = ObjectMapper.Mapper.Map<MenuItemOptionModel>(MenuItemOption);

        //    return MenuItemOptionModel;
        //}

        //public async Task<IEnumerable<MenuItemOptionModel>> GetMenuItemOptionsByName(string name)
        //{
        //    var spec = new MenuItemOptionWithMenuItemOptionesSpecification(name);
        //    var MenuItemOptionList = await _menuItemOptionRepository.GetAsync(spec);

        //    var MenuItemOptionModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemOptionModel>>(MenuItemOptionList);

        //    return MenuItemOptionModels;
        //}

        //public async Task<IEnumerable<MenuItemOptionModel>> GetMenuItemOptionsByCategoryId(int categoryId)
        //{
        //    var spec = new MenuItemOptionWithMenuItemOptionesSpecification(categoryId);
        //    var MenuItemOptionList = await _menuItemOptionRepository.GetAsync(spec);

        //    var MenuItemOptionModels = ObjectMapper.Mapper.Map<IEnumerable<MenuItemOptionModel>>(MenuItemOptionList);

        //    return MenuItemOptionModels;
        //}

        //public async Task<MenuItemOptionModel> CreateMenuItemOption(MenuItemOptionModel MenuItemOption)
        //{
        //    var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(MenuItemOption.Id);
        //    if (existingMenuItemOption != null)
        //    {
        //        throw new ApplicationException("MenuItemOption with this id already exists");
        //    }

        //    var newMenuItemOption = ObjectMapper.Mapper.Map<MenuItemOption>(MenuItemOption);
        //    newMenuItemOption = await _menuItemOptionRepository.SaveAsync(newMenuItemOption);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newMenuItemOptionModel = ObjectMapper.Mapper.Map<MenuItemOptionModel>(newMenuItemOption);
        //    return newMenuItemOptionModel;
        //}

        //public async Task UpdateMenuItemOption(MenuItemOptionModel MenuItemOption)
        //{
        //    var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(MenuItemOption.Id);
        //    if (existingMenuItemOption == null)
        //    {
        //        throw new ApplicationException("MenuItemOption with this id is not exists");
        //    }

        //    existingMenuItemOption.Name = MenuItemOption.Name;
        //    existingMenuItemOption.Description = MenuItemOption.Description;

        //    await _menuItemOptionRepository.SaveAsync(existingMenuItemOption);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteMenuItemOptionById(int MenuItemOptionId)
        //{
        //    var existingMenuItemOption = await _menuItemOptionRepository.GetByIdAsync(MenuItemOptionId);
        //    if (existingMenuItemOption == null)
        //    {
        //        throw new ApplicationException("MenuItemOption with this id is not exists");
        //    }

        //    await _menuItemOptionRepository.DeleteAsync(existingMenuItemOption);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
