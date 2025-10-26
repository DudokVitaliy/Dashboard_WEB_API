using Dashboard_WEB_API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes
{
    public class GenreRepository
        : GenericRepository<GenreEntity, string>, IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            return await _context.Genres
                .AnyAsync(g => g.NormalizedName == name.ToUpper());
        }
    }
}
