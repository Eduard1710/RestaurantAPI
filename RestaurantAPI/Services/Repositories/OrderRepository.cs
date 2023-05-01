using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    partial class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly RestaurantContext _context;
        public OrderRepository(RestaurantContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order? GetOrderDetails(int OrderId)
        {
            return _context.Orders
                .Where(o => o.ID == OrderId && (o.Deleted == false || o.Deleted == null))
                .Include(o => o.User)
                .Include(o=>o.OrderMenu)
                .FirstOrDefault();
        }
        public PagedResult<Order> GetPaginatedList(int pageNumber, int pageSize)
        {
            var totalItems = _context.Orders.Where(o => o.Deleted == false || o.Deleted == null).Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var orders = _context.Orders
                .Where(o => o.Deleted == false || o.Deleted == null)
                .OrderBy(e => e.Address)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Order>
            {
                Items = orders,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public int GetTotalCount()
        {
            return _context.Orders.Where(o => o.Deleted == false || o.Deleted == null).Count();
        }
    }
}
