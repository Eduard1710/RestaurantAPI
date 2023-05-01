using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;
using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.Repositories
{
    partial class OrderMenuRepository : Repository<OrderMenu>, IOrderMenuRepository
    {
        private readonly RestaurantContext _context;
        public OrderMenuRepository(RestaurantContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public OrderMenu? GetOrderMenuDetails(int OrderMenuId)
        {
            return _context.OrderMenus
                .Where(o => o.ID == OrderMenuId && (o.Deleted == false || o.Deleted == null))
                .FirstOrDefault();
        }
    }
}