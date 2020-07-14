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
    public class MenuItemAllergenService : IMenuItemAllergenService
    {
        private readonly IMenuItemAllergenRepository _menuItemAllergenRepository;
        private readonly IAppLogger<MenuItemAllergenService> _logger;
        private readonly IMapper _mapper;

        public MenuItemAllergenService(
            IMenuItemAllergenRepository menuItemAllergenRepository,
            IAppLogger<MenuItemAllergenService> logger,
            IMapper mapper)
        {
            _menuItemAllergenRepository = menuItemAllergenRepository ?? throw new ArgumentNullException(nameof(menuItemAllergenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuItemAllergenModel> Create(MenuItemAllergenModel model)
        {
            var existingMenuItem = await _menuItemAllergenRepository.GetByIdAsync(model.Id);
            if (existingMenuItem != null)
            {
                throw new ApplicationException("menuItem with this id already exists");
            }

            var newmenuItem = _mapper.Map<MenuItemAllergen>(model);
            newmenuItem = await _menuItemAllergenRepository.SaveAsync(newmenuItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemmodel = _mapper.Map<MenuItemAllergenModel>(newmenuItem);
            return newmenuItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingMenuItem = await _menuItemAllergenRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            await _menuItemAllergenRepository.DeleteAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuItemAllergenModel>> Get(int menuItemId)
        {
            var result = await _menuItemAllergenRepository.GetAsync(x => x.MenuItemId == menuItemId);
            return _mapper.Map<IEnumerable<MenuItemAllergenModel>>(result);
        }

        public async Task<MenuItemAllergenModel> GetById(int id)
        {
            return _mapper.Map<MenuItemAllergenModel>(await _menuItemAllergenRepository.GetByIdAsync(id));
        }

        public async Task Update(MenuItemAllergenModel model)
        {
            var existingMenuItem = await _menuItemAllergenRepository.GetByIdAsync(model.Id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            existingMenuItem = _mapper.Map<MenuItemAllergen>(model);

            await _menuItemAllergenRepository.SaveAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }


    }
}
