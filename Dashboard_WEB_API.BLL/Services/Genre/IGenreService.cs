using Dashboard_WEB_API.BLL.Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Genre
{
    public interface IGenreService
    {
        Task<string> CreateAsync(CreateGenreDto dto);
        Task<string> UpdateAsync(UpdateGenreDto dto);
        Task<string> DeleteAsync(string id);
        Task<GenreDto?> GetByIdAsync(string id);
        Task<IEnumerable<GenreDto>> GetAllAsync();
    }
}
