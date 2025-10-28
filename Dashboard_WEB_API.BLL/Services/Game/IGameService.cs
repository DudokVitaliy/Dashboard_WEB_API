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
        Task<ServiceResponce> CreateAsync(CreateGameDto dto);
        Task<ServiceResponce> UpdateAsync(UpdateGameDto dto);
        Task<ServiceResponce> DeleteAsync(string id);
        Task<ServiceResponce> GetByIdAsync(string id);
        Task<ServiceResponce> GetAllAsync();
        Task<ServiceResponce> GetGamesByGenreAsync(string genreId);
    }
}
