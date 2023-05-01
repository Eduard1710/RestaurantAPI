using RestaurantAPI.Entities;

namespace RestaurantAPI.Services.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAdminUsers();
    }
}
