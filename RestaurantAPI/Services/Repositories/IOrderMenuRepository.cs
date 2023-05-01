using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface IOrderMenuRepository: IRepository<OrderMenu>
    {
        OrderMenu? GetOrderMenuDetails(int OrderMenuId);
    }
}
