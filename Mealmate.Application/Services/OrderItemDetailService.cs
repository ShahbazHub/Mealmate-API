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
    public class OrderItemDetailService : IOrderItemDetailService
    {
        private readonly IOrderItemDetailRepository _orderitemdetailRepository;
        private readonly IAppLogger<OrderItemDetailService> _logger;
        private readonly IMapper _mapper;

        public OrderItemDetailService(
            IOrderItemDetailRepository orderitemdetailRepository,
            IAppLogger<OrderItemDetailService> logger,
            IMapper mapper)
        {
            _orderitemdetailRepository = orderitemdetailRepository ?? throw new ArgumentNullException(nameof(_orderitemdetailRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OrderItemDetailModel> Create(OrderItemDetailModel model)
        {
            var existingTable = await _orderitemdetailRepository.GetByIdAsync(model.Id);
            if (existingTable != null)
            {
                throw new ApplicationException("_dietary with this id already exists");
            }

            var new_dietary = _mapper.Map<OrderItemDetail>(model);
            new_dietary = await _orderitemdetailRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<OrderItemDetailModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _orderitemdetailRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderItemDetail with this id is not exists");
            }

            await _orderitemdetailRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OrderItemDetailModel>> Get(int orderItemlId)
        {
            var result = await _orderitemdetailRepository.GetAsync(p => p.OrderItemId == orderItemlId);
            return _mapper.Map<IEnumerable<OrderItemDetailModel>>(result);
        }

        public async Task<OrderItemDetailModel> GetById(int id)
        {
            return _mapper.Map<OrderItemDetailModel>(await _orderitemdetailRepository.GetByIdAsync(id));
        }

        public async Task Update(OrderItemDetailModel model)
        {
            var existingTable = await _orderitemdetailRepository.GetByIdAsync(model.Id);
            if (existingTable == null)
            {
                throw new ApplicationException("OrderItemDetail with this id is not exists");
            }

            existingTable = _mapper.Map<OrderItemDetail>(model);

            await _orderitemdetailRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OrderItemDetailModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _orderitemdetailRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var OrderItemDetailModels = _mapper.Map<List<OrderItemDetailModel>>(TablePagedList.Items);

            var OrderItemDetailModelPagedList = new PagedList<OrderItemDetailModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                OrderItemDetailModels);

            return OrderItemDetailModelPagedList;
        }


    }
}
