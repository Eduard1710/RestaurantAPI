using RestaurantAPI.Entities;
using RestaurantAPI.ExternalModels;

namespace RestaurantAPI.Services.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAdminUsers();
        User? GetUserByEmail(string email);
    }
}
