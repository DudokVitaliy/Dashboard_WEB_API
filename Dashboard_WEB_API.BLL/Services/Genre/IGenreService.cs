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
        Task<ServiceResponce> CreateAsync(CreateGenreDto dto);
        Task<ServiceResponce> UpdateAsync(UpdateGenreDto dto);
        Task<ServiceResponce> DeleteAsync(string id);
        Task<ServiceResponce> GetByIdAsync(string id);
        Task<ServiceResponce> GetAllAsync();
    }
}
