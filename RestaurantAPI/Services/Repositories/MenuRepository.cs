using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    partial class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly RestaurantContext _context;
        public MenuRepository(RestaurantContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Menu? GetMenuDetails(int MenuId)
        {
            return _context.Menus
                .Where(m => m.ID == MenuId && (m.Deleted == false || m.Deleted == null))
                .Include(m => m.Category)
                .FirstOrDefault();
        }
    }
}
