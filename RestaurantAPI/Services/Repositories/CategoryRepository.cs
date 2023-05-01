using RestaurantAPI.Contexts;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    partial class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly RestaurantContext _context;
        public CategoryRepository(RestaurantContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Category? GetCategoryDetails(int categoryId)
        {
            return _context.Categories
                .Where(b => b.ID == categoryId && (b.Deleted == false || b.Deleted == null))
                .FirstOrDefault();
        }
    }
}
