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
        Task<ServiceResponse> CreateAsync(CreateGenreDto dto);
        Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto);
        Task<ServiceResponse> DeleteAsync(string id);
        Task<ServiceResponse> GetByIdAsync(string id);
        Task<ServiceResponse> GetAllAsync();
    }
}
