using RestaurantAPI.Contexts;
using RestaurantAPI.Services.UnitsOfWork;
using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.UnitsOfWork
{
    partial class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly RestaurantContext _context;
        public UserUnitOfWork(RestaurantContext context, IUserRepository users)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Users = users ?? throw new ArgumentNullException(nameof(users));
        }
        public IUserRepository Users { get; }

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
