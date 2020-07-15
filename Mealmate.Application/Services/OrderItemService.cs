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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderitemRepository;
        private readonly IAppLogger<OrderItemService> _logger;
        private readonly IMapper _mapper;

        public OrderItemService(
            IOrderItemRepository orderitemRepository,
            IAppLogger<OrderItemService> logger,
            IMapper mapper)
        {
            _orderitemRepository = orderitemRepository ?? throw new ArgumentNullException(nameof(_orderitemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OrderItemModel> Create(OrderItemModel model)
        {
            var existingTable = await _orderitemRepository.GetByIdAsync(model.Id);
            if (existingTable != null)
            {
                throw new ApplicationException("_dietary with this id already exists");
            }

            var new_dietary = _mapper.Map<OrderItem>(model);
            new_dietary = await _orderitemRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<OrderItemModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _orderitemRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderItem with this id is not exists");
            }

            await _orderitemRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OrderItemModel>> Get(int orderitemId)
        {
            var result = await _orderitemRepository.GetAsync(p => p.OrderId == orderitemId);
            return _mapper.Map<IEnumerable<OrderItemModel>>(result);
        }

        public async Task<OrderItemModel> GetById(int id)
        {
            return _mapper.Map<OrderItemModel>(await _orderitemRepository.GetByIdAsync(id));
        }

        public async Task Update(OrderItemModel model)
        {
            var existingTable = await _orderitemRepository.GetByIdAsync(model.Id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderItem with this id is not exists");
            }

            existingTable = _mapper.Map<OrderItem>(model);

            await _orderitemRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OrderItemModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _orderitemRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var OrderItemModels = _mapper.Map<List<OrderItemModel>>(TablePagedList.Items);

            var OrderItemModelPagedList = new PagedList<OrderItemModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                OrderItemModels);

            return OrderItemModelPagedList;
        }


    }
}
