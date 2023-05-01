using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.UnitsOfWork
{
    public interface IOrderUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        int Complete();
    }
}
