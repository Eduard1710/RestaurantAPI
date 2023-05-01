using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface IMenuRepository:IRepository<Menu>
    {
        Menu? GetMenuDetails(int MenuId);
    }
}
