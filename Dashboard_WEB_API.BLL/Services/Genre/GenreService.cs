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
        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponse
                { 
                    Message = $"Жанр з назвою {dto.Name} вже існує",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };

            }
            var entity = _mapper.Map<GenreEntity>(dto);
            await _genreRepository.CreateAsync(entity);
            return new ServiceResponse
            { 
                Message = $"Жанр з назвою {dto.Name} успішно створено",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.Created
            };
        }
        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode =  System.Net.HttpStatusCode.BadRequest
                };
            }
            await _genreRepository.DeleteAsync(entity);
            return new ServiceResponse
            {
                Message = $"Жанр з id: {id} успішно видалено"
            };
        }
        public async Task <ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return new ServiceResponse
                { 
                    Message = $"Жанр з назвою {dto.Name} вже існує",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id: {dto.Id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            // dto -> entity
            entity = _mapper.Map(dto, entity);

            await _genreRepository.UpdateAsync(entity);
            return new ServiceResponse
            {
                Message = $"Жанр з id: {dto.Id} успішно оновлено"
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Жанр з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            var dto = _mapper.Map<GenreDto>(entity);
            return new ServiceResponse
            {
                Message = $"Жанр '{dto.Name}' успішно отримано",
                Data = dto
            };
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _genreRepository.GetAll().ToListAsync();
            var dtos = entities
                .Select(entity => _mapper.Map<GenreDto>(entity))
                .ToList();
            return new ServiceResponse
            {
                Message = "Жанри успішно отримано",
                Data = dtos
            };
        }
    }
}
