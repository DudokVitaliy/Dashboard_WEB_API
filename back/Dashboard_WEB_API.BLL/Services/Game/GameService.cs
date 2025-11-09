using AutoMapper;
using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.BLL.Services.Storage;
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
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public GameService(IGameRepository gameRepository, 
            IGenreRepository genreRepository, IMapper mapper, IStorageService storageService)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _storageService = storageService;
        }

        public async Task<ServiceResponse> CreateAsync(CreateGameDto dto, string imagePath)
        {
            var entity = _mapper.Map<GameEntity>(dto);
            string gamePath = Path.Combine(imagePath, entity.Id);
            Directory.CreateDirectory(gamePath);

            // Збереження головного зображення
            var mainImageName = await _storageService.SaveImageAsync(dto.MainImage, gamePath);
            if (mainImageName == null)
            {
                return new ServiceResponse
                {
                    Message = "Не вдалося зберегти головне зображення гри",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

            var mainImage = new GameImageEntity
            {
                GameId = entity.Id,
                ImagePath = Path.Combine(entity.Id, mainImageName),
                IsMain = true
            };
            entity.Images.Add(mainImage);

            // Збереження додаткових зображень
            var ImageNames = await _storageService.SaveImagesAsync(dto.Images, gamePath);
            if (ImageNames.Count() == 0 )
            {
                return new ServiceResponse
                {
                    Message = "Не вдалося зберегти додаткові зображення гри",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
            var images = ImageNames.Select(name => new GameImageEntity
            {
                GameId = entity.Id,
                ImagePath = Path.Combine(entity.Id, name),
                IsMain = false
            });
            foreach (var img in images)
            {
                entity.Images.Add(img);
            }
            await _gameRepository.CreateAsync(entity);


            return new ServiceResponse
            {
                Message = $"Гру з назвою {dto.Name} успішно створено",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };

            //var genres = new List<GenreEntity>();
            //foreach (var genreId in dto.GenreId ?? [])
            //{
            //    var genre = await _genreRepository.GetByIdAsync(genreId);
            //    if (genre != null)
            //        genres.Add(genre);
            //}

            //var entity = new GameEntity
            //{
            //    Name = dto.Name,
            //    Description = dto.Description,
            //    Price = dto.Price,
            //    ReleaseDate = DateTime.SpecifyKind(dto.ReleaseDate, DateTimeKind.Utc),
            //    Publisher = dto.Publisher,
            //    Developer = dto.Developer,
            //    Genres = genres
            //};

            //entity.Images = (dto.ImageUrl ?? []).Select((path, index) => new GameImageEntity
            //{
            //    ImagePath = path,
            //    IsMain = index == 0,
            //    GameId = entity.Id
            //}).ToList();

            //await _gameRepository.CreateAsync(entity);

            //return new ServiceResponse
            //{
            //    Message = $"Гру з назвою {dto.Name} успішно створено",
            //};
        }
        public async Task<ServiceResponse> UpdateAsync(UpdateGameDto dto)
        {
            var entity = await _gameRepository.GetByIdAsync(dto.Id);
            if (entity == null)
            {
                return new ServiceResponse
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
            return new ServiceResponse
            {
                Message = $"Гру з id: {dto.Id} успішно оновлено"
            };
        }
        public async Task<ServiceResponse> DeleteAsync(string id, string imagePath)
        {
            var entity = await _gameRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return new ServiceResponse
                { 
                    Message = $"Гру з id: {id} не знайдено",
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            await _storageService.DeleteAllImagesAsync(Path.Combine(imagePath, id));
            await _gameRepository.DeleteAsync(entity);

            return new ServiceResponse
            {
                Message = $"Гру з id: {id} успішно видалено",
                IsSuccess = true,
                HttpStatusCode = System.Net.HttpStatusCode.OK
            };
        }
        public async Task<ServiceResponse> GetByIdAsync(string id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
            {
                return null;
            }
            return new ServiceResponse
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
        public async Task<ServiceResponse> GetAllAsync()
        {
            var games = await _gameRepository.GetAll().ToListAsync();
            return new ServiceResponse
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
        public async Task<ServiceResponse> GetGamesByGenreAsync(string genreId)
        {
            var games = await _gameRepository.GetByGenreAsync(genreId);
            return new ServiceResponse
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
