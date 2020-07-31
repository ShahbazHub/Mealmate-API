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
    public class OrderStateService : IOrderStateService
    {
        private readonly IOrderStateRepository _orderStateRepository;
        private readonly IAppLogger<OrderStateService> _logger;
        private readonly IMapper _mapper;

        public OrderStateService(
            IOrderStateRepository orderStateRepository,
            IAppLogger<OrderStateService> logger,
            IMapper mapper)
        {
            _orderStateRepository = orderStateRepository ?? throw new ArgumentNullException(nameof(_orderStateRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OrderStateModel> Create(OrderStateCreateModel model)
        {

            var new_dietary = new OrderState
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };

            new_dietary = await _orderStateRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<OrderStateModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _orderStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderState with this id is not exists");
            }

            existingTable.IsActive = false;

            await _orderStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OrderStateModel>> Get()
        {
            var result = await _orderStateRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<OrderStateModel>>(result);
        }

        public async Task<OrderStateModel> GetById(int id)
        {
            OrderStateModel model = null;
            var result = await _orderStateRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<OrderStateModel>(result);
            return model;
        }

        public async Task Update(int id, OrderStateUpdateModel model)
        {
            var existingTable = await _orderStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderState with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;

            await _orderStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OrderStateModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _orderStateRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var OrderStateModels = _mapper.Map<List<OrderStateModel>>(TablePagedList.Items);

            var OrderStateModelPagedList = new PagedList<OrderStateModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                OrderStateModels);

            return OrderStateModelPagedList;
        }


    }
}
