using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Services.UnitsOfWork;
using RestaurantAPI.ExternalModels;
using RestaurantAPI.Infrastructure.Exceptions;

namespace RestaurantAPI.Services.Managers
{
    public class OrderService
    {
        private readonly IOrderUnitOfWork _orderUnit;
        private readonly IMenuUnitOfWork _menuUnit;
        private readonly IMapper _mapper;

        public OrderService(IOrderUnitOfWork orderunit, IMenuUnitOfWork menuunit, IMapper mapper)
        {
            _orderUnit = orderunit ?? throw new ArgumentNullException(nameof(orderunit));
            _menuUnit = menuunit ?? throw new ArgumentNullException(nameof(menuunit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public OrderDTO GetOrder(int id)
        {
            var orderEntity = _orderUnit.Orders.Get(id);
            if (orderEntity == null)
                throw new ResourceMissingException($"Order with id {id} doesn't exist!");
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public List<OrderDTO> GetAllOrders()
        {
            var orderEntities = _orderUnit.Orders.Find(o => o.Deleted == false || o.Deleted == null);
            if (orderEntities == null)
                throw new ResourceMissingException("No order found!");
            return _mapper.Map<List<OrderDTO>>(orderEntities);
        }

        public OrderDTO GetOrderDetails(int id)
        {
            var orderEntity = _orderUnit.Orders.GetOrderDetails(id);
            if (orderEntity == null)
                throw new ResourceMissingException($"Order with id {id} doesn't exist!");
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public OrderDTO AddOrder(OrderDTO order)
        {
            var orderEntity = _mapper.Map<Order>(order);

            var listIds = _menuUnit.Menus.GetRecords().Select(c => c.ID).ToList();

            var noMenu = order.OrderMenu
                .All(c => listIds.Contains(c.MenuID));

            if (!noMenu)
                throw new ResourceMissingException($"One of the menus doesn't exist!");

            _orderUnit.Orders.Add(orderEntity);
            _orderUnit.Complete();
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public bool DeleteOrder(int id)
        {
            var orderEntity = _orderUnit.Orders.Get(id);
            if (orderEntity == null)
                throw new ResourceMissingException($"Order with id {id} doesn't exist!");
            orderEntity.Deleted = true;
            _orderUnit.Orders.Remove(orderEntity);
            _orderUnit.Complete();
            return true;
        }

        public OrderDTO UpdateOrder(UpdateOrderDTO order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            if (orderEntity == null)
                throw new ResourceMissingException($"Order with id {order.ID} doesn't exist!");
            _orderUnit.Orders.Update(orderEntity);
            _orderUnit.Complete();
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public PagedResult<Order> GetAllOrdersOrderedByAddress(int pageNumber, int pageSize)
        {
            var totalItems = _orderUnit.Orders.GetTotalCount();
            if (totalItems == 0)
                throw new ResourceMissingException("No order found!");
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var orders = _orderUnit.Orders.GetPaginatedList(pageNumber, pageSize);

            return new PagedResult<Order>
            {
                Items = orders.Items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

    }
}
