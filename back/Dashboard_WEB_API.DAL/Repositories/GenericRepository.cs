using Dashboard_WEB_API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.DAL.Repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : class, IBaseEntity<TId>
        where TId : notnull
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                await _context.Set<TEntity>().AddAsync(entity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            var entity = await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return entity;
        }
    }
}
