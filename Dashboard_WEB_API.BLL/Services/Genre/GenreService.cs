using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponce> CreateAsync(CreateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponce
                { 
                    Message = $"Жанр з назвою {dto.Name} вже існує",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };

            }
            var entity = new GenreEntity
            {
                Name = dto.Name,
                NormalizedName = dto.Name.ToUpper()
                
            };
            await _genreRepository.CreateAsync(entity);
            return new ServiceResponce
            { 
                Message = $"Жанр з назвою {dto.Name} успішно створено",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.Created
            };
        }
        public async Task<ServiceResponce> DeleteAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponce 
                {
                    Message = $"Жанр з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode =  System.Net.HttpStatusCode.BadRequest
                };
            }
            await _genreRepository.DeleteAsync(entity);
            return new ServiceResponce 
            {
                Message = $"Жанр з id: {id} успішно видалено"
            };
        }
        public async Task <ServiceResponce> UpdateAsync(UpdateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponce 
                { 
                    Message = $"Жанр з назвою {dto.Name} вже існує",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return new ServiceResponce
                {
                    Message = $"Жанр з id: {dto.Id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            // dto -> entity
            entity = _mapper.Map(dto, entity);

            await _genreRepository.UpdateAsync(entity);
            return new ServiceResponce
            {
                Message = $"Жанр з id: {dto.Id} успішно оновлено"
            };
        }
        public async Task<ServiceResponce> GetByIdAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponce 
                {
                    Message = $"Жанр з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var dto = new GenreDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
            return new ServiceResponce 
            {
                Message = $"Жанр '{dto.Name}' успішно отримано",
                Data = dto
            };
        }
        public async Task<ServiceResponce> GetAllAsync()
        {
            var entities = await _genreRepository.GetAll().ToListAsync();
            var dtos = entities.Select(entity => new GenreDto
            {
                Id = entity.Id,
                Name = entity.Name
            });
            return new ServiceResponce
            {
                Message = "Жанри успішно отримано",
                Data = dtos
            };
        }
    }
}
