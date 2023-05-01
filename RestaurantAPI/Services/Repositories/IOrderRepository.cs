using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface IOrderRepository:IRepository<Order>
    {
        Order? GetOrderDetails(int OrderId);
    }
}
