using RestaurantAPI.Services.Repositories;

namespace RestaurantAPI.Services.UnitsOfWork
{
    public interface IMenuUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IMenuRepository Menus { get; }
        int Complete();
    }
}
