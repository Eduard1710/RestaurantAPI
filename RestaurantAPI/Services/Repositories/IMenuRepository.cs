using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface IMenuRepository:IRepository<Menu>
    {
        Menu? GetMenuDetails(int MenuId);
        PagedResult<Menu> GetPaginatedList(int pageNumber, int pageSize);
        int GetTotalCount();
    }
}
