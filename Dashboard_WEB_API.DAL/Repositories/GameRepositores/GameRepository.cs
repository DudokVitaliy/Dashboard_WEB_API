using Dashboard_WEB_API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.DAL.Repositories.GameRepositores
{
    public class GameRepository : GenericRepository<GameEntity, string>, IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<GameEntity> Games => GetAll()
            .Include(g => g.Genres)
            .Include(g => g.Images);

        public async Task<IEnumerable<object>> GetByGenreAsync(string genreId)
        {
            return await Games
                .Where(g => g.Genres.Any(genre => genre.Id == genreId))
                .AsNoTracking()
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.Description,
                    g.Price,
                    g.ReleaseDate,
                    g.Publisher,
                    g.Developer,
                    Genres = g.Genres.Select(ge => ge.Name).ToList(),
                    ImageUrls = g.Images.Select(img => img.ImagePath).ToList()
                })
                .ToListAsync();
        }
    }
}


//public class GamesRepository : IGamesRepository
//{
//    private readonly AppDbContext _context;

//    public GamesRepository(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task CreateAsync(GameEntity game)
//    {
//        await _context.Games.AddAsync(game);
//        await _context.SaveChangesAsync();
//    }

//    public async Task UpdateAsync(GameEntity game)
//    {
//        _context.Games.Update(game);
//        await _context.SaveChangesAsync();
//    }

//    public async Task DeleteAsync(string id)
//    {
//        var game = await _context.Games.FindAsync(id);
//        if (game != null)
//        {
//            _context.Games.Remove(game);
//            await _context.SaveChangesAsync();
//        }
//    }

//    public async Task<GameEntity?> GetByIdAsync(string id)
//    {
//        return await _context.Games
//            .Include(g => g.Genres)
//            .Include(g => g.Images)
//            .AsNoTracking()
//            .FirstOrDefaultAsync(g => g.Id == id);
//    }

//    public async Task<IEnumerable<GameEntity>> GetAllAsync()
//    {
//        return await _context.Games
//            .Include(g => g.Genres)
//            .Include(g => g.Images)
//            .AsNoTracking()
//            .ToListAsync();
//    }

//    public async Task<IEnumerable<GameEntity>> GetByGenreAsync(string genreName)
//    {
//        return await _context.Games
//            .Include(g => g.Genres)
//            .Include(g => g.Images)
//            .Where(g => g.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()))
//            .AsNoTracking()
//            .ToListAsync();
//    }
//}

