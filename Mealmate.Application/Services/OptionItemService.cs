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
        private readonly IOptionItemAllergenRepository _optionItemAllergenRepository;
        private readonly IOptionItemDietaryRepository _optionItemDietaryRepository;
        private readonly IAppLogger<OptionItemService> _logger;
        private readonly IMapper _mapper;

        public OptionItemService(
            IOptionItemRepository optionItemRepository,
            IOptionItemAllergenRepository optionItemAllergenRepository,
            IOptionItemDietaryRepository optionItemDietaryRepository,
            IAppLogger<OptionItemService> logger,
            IMapper mapper)
        {
            _optionItemRepository = optionItemRepository ?? throw new ArgumentNullException(nameof(optionItemRepository));
            _optionItemAllergenRepository = optionItemAllergenRepository ?? throw new ArgumentNullException(nameof(optionItemAllergenRepository));
            _optionItemDietaryRepository = optionItemDietaryRepository ?? throw new ArgumentNullException(nameof(optionItemDietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OptionItemModel> Create(OptionItemCreateModel model)
        {
            var newoptionItem = new OptionItem
            {
                BranchId = model.BranchId,
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };
            newoptionItem = await _optionItemRepository.SaveAsync(newoptionItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newoptionItemmodel = _mapper.Map<OptionItemModel>(newoptionItem);
            return newoptionItemmodel;
        }

        public async Task<OptionItemModel> Create(OptionItemDetailCreateModel model)
        {
            var newoptionItem = new OptionItem
            {
                BranchId = model.BranchId,
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name,
            };

            newoptionItem = await _optionItemRepository.SaveAsync(newoptionItem);
            if (newoptionItem != null)
            {

                foreach (var item in model.Allergens)
                {
                    var temp = new OptionItemAllergen
                    {
                        OptionItemId = newoptionItem.Id,
                        AllergenId = item.AllergenId,
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    await _optionItemAllergenRepository.SaveAsync(temp);
                }

                foreach (var item in model.Dietaries)
                {
                    var temp = new OptionItemDietary
                    {
                        OptionItemId = newoptionItem.Id,
                        DietaryId = item.DietaryId,
                        Created = DateTime.Now,
                        IsActive = true
                    };

                    await _optionItemDietaryRepository.SaveAsync(temp);

                }

                _logger.LogInformation("entity successfully added - mealmateappservice");

            }



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
            var model = await _optionItemRepository.GetByIdAsync(id);
            if (model == null)
            {
                throw new Exception("No data found");
            }

            return _mapper.Map<OptionItemModel>(model);
        }

        public async Task Update(int id, OptionItemUpdateModel model)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem.Name = model.Name;
            existingOptionItem.IsActive = model.IsActive;

            await _optionItemRepository.SaveAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task Update(int id, OptionItemDetailUpdateModel model)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem.Name = model.Name;
            existingOptionItem.IsActive = model.IsActive;
            await _optionItemRepository.SaveAsync(existingOptionItem);

            if (model.Allergens.Count == 0)
            {
                var allergens = await _optionItemAllergenRepository.GetAsync(p => p.OptionItemId == id);
                foreach (var item in allergens)
                {
                    await _optionItemAllergenRepository.DeleteAsync(item);
                }
            }
            else
            {
                var allergens = await _optionItemAllergenRepository.GetAsync(p => p.OptionItemId == id);
                foreach (var item in allergens)
                {
                    await _optionItemAllergenRepository.DeleteAsync(item);
                }

                foreach (var item in model.Allergens)
                {
                    if (item.OptionItemAllergenId != 0)
                    {
                        if (!item.IsActive)
                        {
                            var temp = await _optionItemAllergenRepository.GetByIdAsync(item.OptionItemAllergenId);

                            await _optionItemAllergenRepository.DeleteAsync(temp);
                        }
                    }
                    else
                    {
                        if (item.IsActive)
                        {
                            var temp = new OptionItemAllergen
                            {
                                OptionItemId = id,
                                AllergenId = item.AllergenId,
                                Created = DateTime.Now,
                                IsActive = true
                            };
                            await _optionItemAllergenRepository.SaveAsync(temp);
                        }
                        else
                        {
                            var temp = await _optionItemAllergenRepository.GetByIdAsync(item.OptionItemAllergenId);
                            if (temp != null)
                            {
                                await _optionItemAllergenRepository.DeleteAsync(temp);
                            }
                        }
                    }
                }
            }

            if (model.Dietaries.Count == 0)
            {
                var dietaries = await _optionItemDietaryRepository.GetAsync(p => p.OptionItemId == id);
                foreach (var item in dietaries)
                {
                    await _optionItemDietaryRepository.DeleteAsync(item);
                }
            }
            else
            {
                var dietaries = await _optionItemDietaryRepository.GetAsync(p => p.OptionItemId == id);
                foreach (var item in dietaries)
                {
                    await _optionItemDietaryRepository.DeleteAsync(item);
                }

                foreach (var item in model.Dietaries)
                {
                    if (item.OptionItemDietaryId != 0)
                    {
                        if (!item.IsActive)
                        {
                            var temp = await _optionItemDietaryRepository.GetByIdAsync(item.OptionItemDietaryId);

                            await _optionItemDietaryRepository.DeleteAsync(temp);
                        }
                    }
                    else
                    {
                        if (item.IsActive)
                        {
                            var temp = new OptionItemDietary
                            {
                                OptionItemId = id,
                                DietaryId = item.DietaryId,
                                Created = DateTime.Now,
                                IsActive = true
                            };
                            await _optionItemDietaryRepository.SaveAsync(temp);
                        }
                        else
                        {
                            var temp = await _optionItemDietaryRepository.GetByIdAsync(item.OptionItemDietaryId);
                            if (temp != null)
                            {
                                await _optionItemDietaryRepository.DeleteAsync(temp);
                            }
                        }
                    }
                }
            }

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

        public async Task<IPagedList<OptionItemModel>> Search(int branchId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _optionItemRepository.SearchAsync(branchId, isActive, args);

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
