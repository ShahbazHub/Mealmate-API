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

            try
            {
                var orderItems = new List<OrderItem>();
                foreach (var orderItem in model.OrderItems)
                {
                    orderItems.Add(_mapper.Map<OrderItem>(orderItem));
                }
                var orderEntity = new Order
                {
                    CustomerId = model.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = model.OrderNumber,
                    TableId = model.TableId,
                    OrderStateId = model.OrderStateId,
                    OrderItems = model.OrderItems.Select(oicm => new OrderItem
                    {
                        Created = DateTime.UtcNow,
                        MenuItemId = oicm.MenuItemId,
                        Quantity = oicm.Quantity,
                        Price = oicm.Quantity,
                        OrderItemDetails = oicm.OrderItemDetails.Select(oidcm => new OrderItemDetail
                        {
                            Created = DateTime.UtcNow,
                            MenuItemOptionId = oidcm.MenuItemOptionId,
                            Price = oidcm.Price,
                            Quantity = oidcm.Quantity
                        }).ToList<OrderItemDetail>()


                    }).ToList<OrderItem>()
                };
                _context.Orders.Add(orderEntity);
                var result = await _context.SaveChangesAsync();
                
                var placedOrder = _orderRepository
                                .Table
                                .Where(o => o.Id == orderEntity.Id)
                                .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.OrderItemDetails)
                                .Select(o => new OrderModel
                                {
                                    Id = o.Id,
                                    CustomerId = o.CustomerId,
                                    OrderDate = o.OrderDate,
                                    OrderNumber = o.OrderNumber,
                                    TableId = o.TableId,
                                    OrderStateId = o.OrderStateId,
                                    OrderItems = o.OrderItems.Select(oi => new OrderItemModel
                                    {
                                        Id = oi.Id,
                                        MenuItemId = oi.MenuItemId,
                                        Created = oi.Created,
                                        Price = oi.Price,
                                        Quantity = oi.Quantity,
                                        OrderId = oi.OrderId,
                                        MenuItemName = oi.MenuItem.Name,
                                        MenuItemDescription = oi.MenuItem.Description,
                                        OrderItemDetails = oi.OrderItemDetails.Select(oid => new OrderItemDetailModel
                                        {
                                            Id = oid.Id,
                                            OrderItemId = oid.OrderItemId,
                                            Price = oid.Price,
                                            Quantity = oid.Quantity,
                                            Created = oid.Created,
                                            MenuItemOptionId = oid.MenuItemOptionId,
                                            MenuItemOptionName = oid.MenuItemOption.OptionItem.Name

                                        }).ToList()
                                    }).ToList()
                                }).FirstOrDefault();

                return placedOrder;
            }

            catch (Exception)
            {
                throw new Exception("Error has occured while processing");
            }
        }
        #endregion

        #region Read
        public async Task<IEnumerable<OrderModel>> Get(int customerId)
        {
            var result = await _orderRepository.GetAsync(p => p.CustomerId == customerId);
            return _mapper.Map<IEnumerable<OrderModel>>(result);
        }

        public async Task<IEnumerable<OrderModel>> Get(int branchId, int orderStateId)
        {
            var result = await _context.Orders
                                    .Include(p => p.OrderState)
                                    .Include(p => p.Customer)
                                    .Include(p => p.Table)
                                    .ThenInclude(l => l.Location)
                                    .ThenInclude(b => b.Branch)
                                    .ThenInclude(r => r.Restaurant)
                                    .ToListAsync();

            result = result.Where(p => p.Table.Location.Branch.Id == branchId &&
                                  p.OrderStateId == orderStateId).ToList();
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
                                // inserting new
                                if (orderItem.OrderItemId == 0)
                                {
                                    var orderItemEntity = new OrderItem
                                    {
                                        Created = DateTime.Now,
                                        MenuItemId = orderItem.MenuItemId,
                                        OrderId = id,
                                        Price = orderItem.Price,
                                        Quantity = orderItem.Quantity
                                    };


                                    await _context.OrderItems.AddAsync(orderItemEntity);
                                    if (await _context.SaveChangesAsync() > 0)
                                    {
                                        if (orderItem.OrderItemDetails.Count > 0)
                                        {
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
                                else  // update existing
                                {
                                    var orderItemEntity = await _context.OrderItems.FirstOrDefaultAsync(p => p.Id == orderItem.OrderItemId);
                                    if (orderItemEntity != null)
                                    {
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
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    throw new ApplicationException("Order with this id is not exists");
                }

                var orderItems = await _orderItemRepository.GetAsync(p => p.OrderId == id);
                foreach (var orderItem in orderItems)
                {
                    var orderItemDetails = await _orderItemDetailRepository.GetAsync(p => p.OrderItemId == orderItem.Id);
                    foreach (var orderItemDetail in orderItemDetails)
                    {
                        await _orderItemDetailRepository.DeleteAsync(orderItemDetail);
                    }

                    await _orderItemRepository.DeleteAsync(orderItem);
                }

                await _orderRepository.DeleteAsync(order);

            }
            catch (Exception)
            {
                throw new Exception("Error while deleting");
            }

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }
        #endregion
    }
}
