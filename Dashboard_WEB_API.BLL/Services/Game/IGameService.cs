using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Game
{
    public interface IGameService
    {
        Task<string> CreateAsync(CreateGameDto dto);
        Task<string> UpdateAsync(UpdateGameDto dto);
        Task<string> DeleteAsync(string id);
        Task<GameDto?> GetByIdAsync(string id);
        Task<IEnumerable<GameDto>> GetAllAsync();
        Task<IEnumerable<GameDto>> GetGamesByGenreAsync(string genreId);
    }
}
