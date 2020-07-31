using System;
using System.Collections.Generic;
using System.Linq;
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
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace Mealmate.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly MealmateContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderItemDetailRepository _orderItemDetailRepository;
        private readonly IAppLogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IOrderItemDetailRepository orderItemDetailRepository,
            IAppLogger<OrderService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _orderItemDetailRepository = orderItemDetailRepository ?? throw new ArgumentNullException(nameof(orderItemDetailRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<OrderModel> Create(OrderCreateModel model)
        {
            OrderModel new_model = null;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var orderEntity = new Order
                    {
                        CustomerId = model.CustomerId,
                        OrderDate = DateTime.Now,
                        OrderNumber = model.OrderNumber,
                        TableId = model.TableId,
                        OrderStateId = model.OrderStateId,
                    };

                    await _context.Orders.AddAsync(orderEntity);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        // iterating over all orderitems and process - dishes
                        foreach (var orderItem in model.OrderItems)
                        {
                            var orderItemEntity = new OrderItem
                            {
                                OrderId = orderEntity.Id,   // Primary key of order saved
                                Created = DateTime.Now,
                                MenuItemId = orderItem.MenuItemId,
                                Price = orderItem.Price,
                                Quantity = orderItem.Quantity,
                            };

                            await _context.OrderItems.AddAsync(orderItemEntity);
                            if (await _context.SaveChangesAsync() > 0)
                            {
                                // iterating over all orderitemdetails and process - variations
                                foreach (var orderItemDetail in orderItem.OrderItemDetails)
                                {
                                    var orderItemDetailEntity = new OrderItemDetail
                                    {
                                        OrderItemId = orderItemEntity.Id,  // Primary key of order item saved
                                        Created = DateTime.Now,
                                        MenuItemOptionId = orderItemDetail.MenuItemOptionId,
                                        Price = orderItemDetail.Price,
                                        Quantity = orderItemDetail.Quantity
                                    };

                                    await _context.OrderItems.AddAsync(orderItemEntity);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                    new_model = _mapper.Map<OrderModel>(orderEntity);
                }
                catch (Exception)
                {
                    throw new Exception("Error has occured while processing");
                }
            }

            _logger.LogInformation("entity successfully added - mealmateappservice");
            return new_model;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<OrderModel>> Get(int customerId)
        {
            var result = await _orderRepository.GetAsync(p => p.CustomerId == customerId);
            return _mapper.Map<IEnumerable<OrderModel>>(result);
        }

        public async Task<OrderModel> GetById(int id)
        {
            return _mapper.Map<OrderModel>(await _orderRepository.GetByIdAsync(id));
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
        #endregion

        #region Update
        public async Task Update(int id, OrderUpdateModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingOrder = await _context.Orders.FirstOrDefaultAsync(p => p.Id == id);
                    if (existingOrder == null)
                    {
                        throw new ApplicationException("Order with this id is not exists");
                    }

                    existingOrder.OrderStateId = model.OrderStateId;

                    // Saving order to database
                    _context.Orders.Update(existingOrder);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        if (model.OrderItems.Count > 0)
                        {
                            // Update order items
                            foreach (var orderItem in model.OrderItems)
                            {
                                var orderItemEntity = await _context.OrderItems.FirstOrDefaultAsync(p => p.Id == orderItem.OrderItemId);
                                if (orderItemEntity != null)
                                {
                                    orderItemEntity.Price = orderItem.Price;
                                    orderItemEntity.Quantity = orderItem.Quantity;

                                    // Saving order item to database 
                                    _context.OrderItems.Update(orderItemEntity);
                                    if (await _context.SaveChangesAsync() > 0)
                                    {
                                        if (orderItem.OrderItemDetails.Count > 0)
                                        {
                                            foreach (var orderItemDetail in orderItem.OrderItemDetails)
                                            {
                                                var orderItemDetailEntity = await _context.OrderItemDetails.FirstOrDefaultAsync(p => p.Id == orderItemDetail.OrderItemDetailId);
                                                if (orderItemDetailEntity != null)
                                                {
                                                    orderItemDetailEntity.Price = orderItemDetail.Price;
                                                    orderItemDetailEntity.Quantity = orderItemDetail.Quantity;

                                                    // Saving order item detail to database 

                                                    _context.OrderItemDetails.Update(orderItemDetailEntity);
                                                    await _context.SaveChangesAsync();
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();

                }
                catch (Exception)
                {
                    throw new Exception("Error has occured while processing");
                }
            }
            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
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
        #endregion
    }
}
