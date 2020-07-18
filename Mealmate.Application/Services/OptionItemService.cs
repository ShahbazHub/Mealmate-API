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
    public class OptionItemService : IOptionItemService
    {
        private readonly IOptionItemRepository _optionItemRepository;
        private readonly IAppLogger<OptionItemService> _logger;
        private readonly IMapper _mapper;

        public OptionItemService(
            IOptionItemRepository optionItemRepository,
            IAppLogger<OptionItemService> logger,
            IMapper mapper)
        {
            _optionItemRepository = optionItemRepository ?? throw new ArgumentNullException(nameof(optionItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OptionItemModel> Create(OptionItemModel model)
        {
            var newoptionItem = _mapper.Map<OptionItem>(model);
            newoptionItem = await _optionItemRepository.SaveAsync(newoptionItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newoptionItemmodel = _mapper.Map<OptionItemModel>(newoptionItem);
            return newoptionItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            await _optionItemRepository.DeleteAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OptionItemModel>> Get()
        {
            var result = await _optionItemRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<OptionItemModel>>(result);
        }

        public async Task<OptionItemModel> GetById(int id)
        {
            return _mapper.Map<OptionItemModel>(await _optionItemRepository.GetByIdAsync(id));
        }

        public async Task Update(OptionItemModel model)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(model.Id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem = _mapper.Map<OptionItem>(model);

            await _optionItemRepository.SaveAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OptionItemModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _optionItemRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<OptionItemModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<OptionItemModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<OptionItemModel>> Search(int branchId, PageSearchArgs args)
        {
            var TablePagedList = await _optionItemRepository.SearchAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<OptionItemModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<OptionItemModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }
    }
}
