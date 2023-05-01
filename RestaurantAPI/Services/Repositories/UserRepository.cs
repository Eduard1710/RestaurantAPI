using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    partial class UserRepository : Repository<User>, IUserRepository
    {
        private readonly RestaurantContext _context;
        public UserRepository(RestaurantContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<User> GetAdminUsers()
        {
            return _context.Users
                .Where(u => u.IsAdmin && (u.Deleted == false || u.Deleted == null))
                .ToList();
        }
    }
}
