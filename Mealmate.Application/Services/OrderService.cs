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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAppLogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IAppLogger<OrderService> logger,
            IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OrderModel> Create(OrderModel model)
        {
            var existingTable = await _orderRepository.GetByIdAsync(model.Id);
            if (existingTable != null)
            {
                throw new ApplicationException("_dietary with this id already exists");
            }

            var new_dietary = _mapper.Map<Order>(model);
            new_dietary = await _orderRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<OrderModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _orderRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Order with this id is not exists");
            }

            await _orderRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OrderModel>> Get(int customerId)
        {
            var result = await _orderRepository.GetAsync(p => p.CustomerId == customerId);
            return _mapper.Map<IEnumerable<OrderModel>>(result);
        }

        public async Task<OrderModel> GetById(int id)
        {
            return _mapper.Map<OrderModel>(await _orderRepository.GetByIdAsync(id));
        }

        public async Task Update(OrderModel model)
        {
            var existingTable = await _orderRepository.GetByIdAsync(model.Id);
            if (existingTable == null)
            {
                throw new ApplicationException("Order with this id is not exists");
            }

            existingTable = _mapper.Map<Order>(model);

            await _orderRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OrderModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _orderRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var OrderModels = _mapper.Map<List<OrderModel>>(TablePagedList.Items);

            var OrderModelPagedList = new PagedList<OrderModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                OrderModels);

            return OrderModelPagedList;
        }


    }
}
