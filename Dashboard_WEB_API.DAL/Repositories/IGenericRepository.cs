
using Dashboard_WEB_API.DAL.Entities;

namespace Dashboard_WEB_API.DAL.Repositories
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId>
        where TId : notnull
    {
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(params TEntity[] entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(TId id);
        IQueryable<TEntity> GetAll();
    }
}
