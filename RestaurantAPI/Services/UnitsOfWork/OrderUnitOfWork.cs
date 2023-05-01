using RestaurantAPI.Contexts;
using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.UnitsOfWork
{
    partial class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly RestaurantContext _context;
        public OrderUnitOfWork(RestaurantContext context, IOrderRepository orders)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Orders = orders ?? throw new ArgumentNullException(nameof(orders));        }
        public IOrderRepository Orders { get; }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
