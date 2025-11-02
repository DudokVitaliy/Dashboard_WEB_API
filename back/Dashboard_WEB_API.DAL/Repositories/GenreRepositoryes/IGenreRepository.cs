
using Dashboard_WEB_API.DAL.Entities;

namespace Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes
{
    public interface IGenreRepository
        : IGenericRepository<GenreEntity, string>
    {
        Task<bool> IsExistsAsync(string name);
    }
}
