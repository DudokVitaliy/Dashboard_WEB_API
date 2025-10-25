using Dashboard_WEB_API.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Dashboard_WEB_API.DAL.Repositories.GameRepositores
{
    public class GamesRepository : IGamesRepository
    {
        private readonly AppDbContext _context;

        public GamesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(GameEntity game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GameEntity game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameEntity?> GetByIdAsync(string id)
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Images)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<GameEntity>> GetAllAsync()
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Images)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<GameEntity>> GetByGenreAsync(string genreName)
        {
            return await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.Images)
                .Where(g => g.Genres.Any(genre => genre.Name.ToLower() == genreName.ToLower()))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
