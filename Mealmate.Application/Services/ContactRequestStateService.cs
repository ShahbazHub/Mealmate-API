using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class ContactRequestStateService : IContactRequestStateService
    {
        private readonly IContactRequestStateRepository _contactRequestStateRepository;
        private readonly IAppLogger<ContactRequestStateService> _logger;
        private readonly IMapper _mapper;

        public ContactRequestStateService(
            IContactRequestStateRepository contactRequestStateRepository,
            IAppLogger<ContactRequestStateService> logger,
            IMapper mapper)
        {
            _contactRequestStateRepository = contactRequestStateRepository ?? throw new ArgumentNullException(nameof(contactRequestStateRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<ContactRequestStateModel> Create(ContactRequestStateCreateModel model)
        {

            var new_dietary = new ContactRequestState
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };

            new_dietary = await _contactRequestStateRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<ContactRequestStateModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<ContactRequestStateModel>> Get()
        {
            var result = await _contactRequestStateRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<ContactRequestStateModel>>(result);
        }

        public async Task<ContactRequestStateModel> GetById(int id)
        {
            ContactRequestStateModel model = null;
            var result = await _contactRequestStateRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<ContactRequestStateModel>(result);
            return model;
        }
        public async Task<IPagedList<ContactRequestStateModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _contactRequestStateRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var ContactRequestStateModels = _mapper.Map<List<ContactRequestStateModel>>(TablePagedList.Items);

            var ContactRequestStateModelPagedList = new PagedList<ContactRequestStateModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                ContactRequestStateModels);

            return ContactRequestStateModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, ContactRequestStateUpdateModel model)
        {
            var existingTable = await _contactRequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("ContactRequestState with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;

            await _contactRequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _contactRequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("ContactRequestState with this id is not exists");
            }

            existingTable.IsActive = false;

            await _contactRequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
