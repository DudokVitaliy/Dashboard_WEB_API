using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GameRepositores;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.BLL.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenreRepository _genreRepository;

        public GameService(IGameRepository gameRepository, IGenreRepository genreRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
        }

        public async Task<ServiceResponce> CreateAsync(CreateGameDto dto)
        {
            var genres = new List<GenreEntity>();
            foreach (var genreId in dto.GenreId ?? [])
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                if (genre != null)
                    genres.Add(genre);
            }

            var entity = new GameEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ReleaseDate = DateTime.SpecifyKind(dto.ReleaseDate, DateTimeKind.Utc),
                Publisher = dto.Publisher,
                Developer = dto.Developer,
                Genres = genres
            };

            entity.Images = (dto.ImageUrl ?? []).Select((path, index) => new GameImageEntity
            {
                ImagePath = path,
                IsMain = index == 0,
                GameId = entity.Id
            }).ToList();

            await _gameRepository.CreateAsync(entity);

            return new ServiceResponce
            {
                Message = $"Гру з назвою {dto.Name} успішно створено",
            };
        }
        public async Task<ServiceResponce> UpdateAsync(UpdateGameDto dto)
        {
            var entity = await _gameRepository.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return new ServiceResponce
                {
                    Message = $"Гру з id: {dto.Id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (dto.Name != null && dto.Name != "string")
                entity.Name = dto.Name;
            if (dto.Description != null && dto.Description != "string")
                entity.Description = dto.Description;
            if (dto.Price.HasValue && dto.Price != 0)
                entity.Price = dto.Price.Value;
            if (dto.ReleaseDate.HasValue &&
                (DateTime.UtcNow - dto.ReleaseDate.Value).TotalSeconds > 5)
            {
                entity.ReleaseDate = DateTime.SpecifyKind(dto.ReleaseDate.Value, DateTimeKind.Utc);
            }
            if (dto.Publisher != null && dto.Publisher != "string")
                entity.Publisher = dto.Publisher;
            if (dto.Developer != null && dto.Developer != "string")
                entity.Developer = dto.Developer;
            if (dto.GenreId != null && dto.GenreId.Any() && dto.GenreId.All(id => id != "string"))
            {
                var genres = new List<GenreEntity>();
                foreach (var genreId in dto.GenreId)
                {
                    var genre = await _genreRepository.GetByIdAsync(genreId);
                    if (genre != null)
                        genres.Add(genre);
                }
                entity.Genres = genres;
            }
            if (dto.ImageUrl != null && dto.ImageUrl.Any() && dto.ImageUrl.All(url => url != "string"))
            {
                entity.Images = dto.ImageUrl.Select((path, index) => new GameImageEntity
                {
                    ImagePath = path,
                    IsMain = index == 0,
                    GameId = entity.Id
                }).ToList();
            }
            await _gameRepository.UpdateAsync(entity);
            return new ServiceResponce
            {
                Message = $"Гру з id: {dto.Id} успішно оновлено"
            };
        }
        public async Task<ServiceResponce> DeleteAsync(string id)
        {
            var entity = await _gameRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponce
                { 
                    Message = $"Гру з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            await _gameRepository.DeleteAsync(entity);
            return new ServiceResponce
            {
                Message = $"Гру з id: {id} успішно видалено",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
        public async Task<ServiceResponce> GetByIdAsync(string id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
            {
                return null;
            }
            return new ServiceResponce
            {
                Message = "Успішне отримання гри за id",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Data = new GameDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    ReleaseDate = game.ReleaseDate,
                    Publisher = game.Publisher,
                    Developer = game.Developer,
                    GenreId = game.Genres.Select(g => g.Id).ToList(),
                    ImageUrl = game.Images.Select(i => i.ImagePath).ToList()
                },
            };
        }
        public async Task<ServiceResponce> GetAllAsync()
        {
            var games = await _gameRepository.GetAll().ToListAsync();
            return new ServiceResponce
            {
                Message = "Успішне отримання всіх ігор",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Data = games.Select(game => new GameDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    ReleaseDate = game.ReleaseDate,
                    Publisher = game.Publisher,
                    Developer = game.Developer,
                    GenreId = game.Genres.Select(g => g.Id).ToList(),
                    ImageUrl = game.Images.Select(i => i.ImagePath).ToList()
                }),
            };
        }
        public async Task<ServiceResponce> GetGamesByGenreAsync(string genreId)
        {
            var games = await _gameRepository.GetByGenreAsync(genreId);
            return new ServiceResponce
            {
                Message = "Успішне отримання ігор за жанром",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                Data = games.Select(game => new GameDto
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    ReleaseDate = game.ReleaseDate,
                    Publisher = game.Publisher,
                    Developer = game.Developer,
                    GenreId = game.Genres.Select(g => g.Id).ToList(),
                    ImageUrl = game.Images.Select(i => i.ImagePath).ToList()
                }),
            };
        }
    }
}
