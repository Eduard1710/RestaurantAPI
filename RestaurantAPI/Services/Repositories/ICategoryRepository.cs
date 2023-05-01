using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category? GetCategoryDetails(int categoryId); 
    }
}
