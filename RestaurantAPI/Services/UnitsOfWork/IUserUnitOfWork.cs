using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.UnitsOfWork
{
    public interface IUserUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}
