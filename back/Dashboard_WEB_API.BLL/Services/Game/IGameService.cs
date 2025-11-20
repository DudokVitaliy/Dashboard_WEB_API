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
        Task<ServiceResponse> CreateAsync(CreateGameDto dto, string imagePath);
        Task<ServiceResponse> UpdateAsync(UpdateGameDto dto, string imagePath);
        Task<ServiceResponse> DeleteAsync(string id, string imagePath);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetGamesByGenreAsync(string genreId);
    }
}
