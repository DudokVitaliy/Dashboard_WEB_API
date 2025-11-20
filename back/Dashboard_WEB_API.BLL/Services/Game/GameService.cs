using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Services.Storage;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GameRepositores;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Dashboard_WEB_API.BLL.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public GameService(
            IGameRepository gameRepository,
            IGenreRepository genreRepository,
            IMapper mapper,
            IStorageService storageService)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _storageService = storageService;
        }

        // ===================================================
        // CREATE GAME
        // ===================================================
        public async Task<ServiceResponse> CreateAsync(CreateGameDto dto, string imageRoot)
        {
            var entity = _mapper.Map<GameEntity>(dto);

            var genres = new List<GenreEntity>();
            foreach (var genreId in dto.GenreId ?? [])
            {
                var g = await _genreRepository.GetByIdAsync(genreId);
                if (g != null) genres.Add(g);
            }
            entity.Genres = genres;

            string gameDir = Path.Combine(imageRoot, entity.Id);
            Directory.CreateDirectory(gameDir);

            var mainImageName = await _storageService.SaveImageAsync(dto.MainImage, gameDir);
            if (mainImageName == null)
            {
                return new ServiceResponse
                {
                    Message = "Не вдалося зберегти головне зображення гри",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.InternalServerError
                };
            }

            entity.Images.Add(new GameImageEntity
            {
                GameId = entity.Id,
                ImagePath = Path.Combine(entity.Id, mainImageName),
                IsMain = true
            });

            var imageNames = await _storageService.SaveImagesAsync(dto.Images, gameDir);

            foreach (var name in imageNames)
            {
                entity.Images.Add(new GameImageEntity
                {
                    GameId = entity.Id,
                    ImagePath = Path.Combine(entity.Id, name),
                    IsMain = false
                });
            }

            await _gameRepository.CreateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Гру '{dto.Name}' успішно створено",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        // ===================================================
        // UPDATE GAME
        // ===================================================
        public async Task<ServiceResponse> UpdateAsync(UpdateGameDto dto, string imageRoot)
        {
            var entity = await _gameRepository.Games
                .Include(g => g.Images)
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(g => g.Id == dto.Id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Гру з id: {dto.Id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name != "string") entity.Name = dto.Name;
            if (!string.IsNullOrWhiteSpace(dto.Description) && dto.Description != "string") entity.Description = dto.Description;
            if (dto.Price.HasValue && dto.Price != 0) entity.Price = dto.Price.Value;
            if (dto.ReleaseDate.HasValue && dto.ReleaseDate.Value.Date != DateTime.UtcNow.Date) entity.ReleaseDate = dto.ReleaseDate.Value;
            if (!string.IsNullOrWhiteSpace(dto.Developer) && dto.Developer != "string") entity.Developer = dto.Developer;
            if (!string.IsNullOrWhiteSpace(dto.Publisher) && dto.Publisher != "string") entity.Publisher = dto.Publisher;

            if (dto.GenreId != null)
            {
                entity.Genres.Clear();

                foreach (var genreId in dto.GenreId)
                {
                    var genre = await _genreRepository.GetByIdAsync(genreId);
                    if (genre != null) entity.Genres.Add(genre);
                }
            }

            string gameDir = Path.Combine(imageRoot, entity.Id);
            Directory.CreateDirectory(gameDir);

            if (dto.DeleteImageIds != null && dto.DeleteImageIds.Any())
            {
                var toDelete = entity.Images
                    .Where(i => dto.DeleteImageIds.Contains(i.Id))
                    .ToList();

                foreach (var img in toDelete)
                {
                    string fullPath = Path.Combine(imageRoot, img.ImagePath);
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);

                    entity.Images.Remove(img);
                }
            }

            if (dto.NewMainImage != null)
            {
                var oldMain = entity.Images.FirstOrDefault(i => i.IsMain);
                if (oldMain != null)
                {
                    string fullPath = Path.Combine(imageRoot, oldMain.ImagePath);
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);

                    entity.Images.Remove(oldMain);
                }

                var fileName = await _storageService.SaveImageAsync(dto.NewMainImage, gameDir);

                entity.Images.Add(new GameImageEntity
                {
                    GameId = entity.Id,
                    ImagePath = Path.Combine(entity.Id, fileName),
                    IsMain = true
                });
            }

            if (dto.NewImages != null && dto.NewImages.Any())
            {
                var fileNames = await _storageService.SaveImagesAsync(dto.NewImages, gameDir);

                foreach (var f in fileNames)
                {
                    entity.Images.Add(new GameImageEntity
                    {
                        GameId = entity.Id,
                        ImagePath = Path.Combine(entity.Id, f),
                        IsMain = false
                    });
                }
            }

            await _gameRepository.UpdateAsync(entity);

            return new ServiceResponse
            {
                Message = $"Гру '{entity.Name}' успішно оновлено",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        // ===================================================
        // DELETE GAME
        // ===================================================
        public async Task<ServiceResponse> DeleteAsync(string id, string imageRoot)
        {
            var entity = await _gameRepository.Games
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Message = $"Гру з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            entity.Genres.Clear();

            string folder = Path.Combine(imageRoot, id);
            await _storageService.DeleteAllImagesAsync(folder);

            await _gameRepository.DeleteAsync(entity);

            return new ServiceResponse
            {
                Message = $"Гру '{entity.Name}' успішно видалено",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK
            };
        }

        // ===================================================
        // GET BY ID
        // ===================================================
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var game = await _gameRepository.Games
                .Include(g => g.Images)
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return new ServiceResponse
                {
                    Message = $"Гру з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
            }

            return new ServiceResponse
            {
                Message = "Успішно отримано гру",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK,
                Data = _mapper.Map<GameDto>(game)
            };
        }

        // ===================================================
        // GET ALL
        // ===================================================
        public async Task<ServiceResponse> GetAllAsync()
        {
            var games = await _gameRepository.Games
                .Include(g => g.Images)
                .Include(g => g.Genres)
                .ToListAsync();

            return new ServiceResponse
            {
                Message = "Успішно отримано всі ігри",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK,
                Data = _mapper.Map<List<GameDto>>(games)
            };
        }

        // ===================================================
        // GET BY GENRE
        // ===================================================
        public async Task<ServiceResponse> GetGamesByGenreAsync(string genreId)
        {
            var games = await _gameRepository.GetByGenreAsync(genreId);

            return new ServiceResponse
            {
                Message = "Успішно отримано ігри за жанром",
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK,
                Data = _mapper.Map<List<GameDto>>(games)
            };
        }
    }
}
