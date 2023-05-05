using System.Linq.Expressions;

namespace RestaurantAPI.Services.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity? FindDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        TEntity? Get(int id);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        bool Any(Func<TEntity, bool> expression);
        IQueryable<TEntity> GetRecords();
    }
}
