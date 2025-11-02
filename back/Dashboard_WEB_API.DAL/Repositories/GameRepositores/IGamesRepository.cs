using Dashboard_WEB_API.DAL.Entities;

namespace Dashboard_WEB_API.DAL.Repositories.GameRepositores
{
    public interface IGameRepository : IGenericRepository<GameEntity, string>
    {
        IQueryable<GameEntity> Games { get; }

        Task<IEnumerable<GameEntity>> GetByGenreAsync(string genreId);
    }

    //public interface IGamesRepository
    //{
    //    Task CreateAsync(GameEntity game);
    //    Task UpdateAsync(GameEntity game);
    //    Task DeleteAsync(string id);
    //    Task<GameEntity?> GetByIdAsync(string id);
    //    Task<IEnumerable<GameEntity>> GetAllAsync();
    //    Task<IEnumerable<GameEntity>> GetByGenreAsync(string genreName);
    //}
}
