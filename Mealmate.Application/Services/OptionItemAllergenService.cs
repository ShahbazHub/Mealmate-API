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
    public class OptionItemAllergenService : IOptionItemAllergenService
    {
        private readonly IOptionItemAllergenRepository _optionItemAllergenRepository;
        private readonly IAppLogger<OptionItemAllergenService> _logger;
        private readonly IMapper _mapper;

        public OptionItemAllergenService(
            IOptionItemAllergenRepository optionItemAllergenRepository,
            IAppLogger<OptionItemAllergenService> logger,
            IMapper mapper)
        {
            _optionItemAllergenRepository = optionItemAllergenRepository ?? throw new ArgumentNullException(nameof(optionItemAllergenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OptionItemAllergenModel> Create(OptionItemAllergenModel model)
        {
            var existingOptionItem = await _optionItemAllergenRepository.GetByIdAsync(model.Id);
            if (existingOptionItem != null)
            {
                throw new ApplicationException("optionItem with this id already exists");
            }

            var newoptionItem = _mapper.Map<OptionItemAllergen>(model);
            newoptionItem = await _optionItemAllergenRepository.SaveAsync(newoptionItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newoptionItemmodel = _mapper.Map<OptionItemAllergenModel>(newoptionItem);
            return newoptionItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingOptionItem = await _optionItemAllergenRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            await _optionItemAllergenRepository.DeleteAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OptionItemAllergenModel>> Get(int optionItemId)
        {
            var result = await _optionItemAllergenRepository.GetAsync(x => x.OptionItemId == optionItemId);
            return _mapper.Map<IEnumerable<OptionItemAllergenModel>>(result);
        }

        public async Task<OptionItemAllergenModel> GetById(int id)
        {
            return _mapper.Map<OptionItemAllergenModel>(await _optionItemAllergenRepository.GetByIdAsync(id));
        }

        public async Task Update(OptionItemAllergenModel model)
        {
            var existingOptionItem = await _optionItemAllergenRepository.GetByIdAsync(model.Id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem = _mapper.Map<OptionItemAllergen>(model);

            await _optionItemAllergenRepository.SaveAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OptionItemAllergenModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _optionItemAllergenRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<OptionItemAllergenModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<OptionItemAllergenModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<OptionItemAllergenModel>> Search(int branchId, PageSearchArgs args)
        {
            var TablePagedList = await _optionItemAllergenRepository.SearchAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<OptionItemAllergenModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<OptionItemAllergenModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }
    }
}
