using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;

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

        public PagedResult<Menu> GetPaginatedList(int pageNumber, int pageSize)
        {
            var totalItems = _context.Menus.Where(o => o.Deleted == false || o.Deleted == null).Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var menus = _context.Menus
                .Where(o => o.Deleted == false || o.Deleted == null)
                .OrderBy(e => e.MenuName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Menu>
            {
                Items = menus,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }

        public int GetTotalCount()
        {
            return _context.Menus.Where(o => o.Deleted == false || o.Deleted == null).Count();
        }
    }
}
