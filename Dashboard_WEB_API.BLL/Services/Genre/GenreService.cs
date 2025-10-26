using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<string> CreateAsync(CreateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return $"Жанр з назвою {dto.Name} вже існує";
            }
            var entity = new GenreEntity
            {
                Name = dto.Name,
                NormalizedName = dto.Name.ToUpper()
                
            };
            await _genreRepository.CreateAsync(entity);
            return $"Жанр {dto.Name} створено";
        }
        public async Task<string> DeleteAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return $"Жанр з id: {id} не знайдено";
            }
            await _genreRepository.DeleteAsync(entity);
            return $"Жанр з id: {id} видалено";
        }
        public async Task <string> UpdateAsync(UpdateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return $"Жанр з назвою {dto.Name} вже існує";
            }

            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return $"Жанр з id: {dto.Id} не знайдено";
            }
            entity.Name = dto.Name;
            entity.NormalizedName = dto.Name.ToUpper();
            await _genreRepository.UpdateAsync(entity);
            return $"Жанр з id: {dto.Id} оновлено";
        }
        public async Task<GenreDto?> GetByIdAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = new GenreDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return dto;
        }
        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var entities = await _genreRepository.GetAll().ToListAsync();
            var dtos = entities.Select(entity => new GenreDto
            {
                Id = entity.Id,
                Name = entity.Name
            });
            return dtos;
        }
    }
}
