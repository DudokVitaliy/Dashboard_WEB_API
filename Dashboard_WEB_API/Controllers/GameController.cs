using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GameRepositores;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.Controllers
{
    public class CreateGameRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public List<string> GenreId { get; set; } = [];
        public List<string> ImageUrl { get; set; } = [];
    }

    public class UpdateGameRequest
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public List<string>? GenreId { get; set; }
        public List<string>? ImageUrl { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IGenreRepository _genreRepository;

        public GameController(IGameRepository gameRepository, IGenreRepository genreRepository)
        {
            _gameRepository = gameRepository;
            _genreRepository = genreRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameRequest request)
        {
            var genres = new List<GenreEntity>();
            foreach (var genreId in request.GenreId)
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                if (genre != null)
                    genres.Add(genre);
            }

            var entity = new GameEntity
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ReleaseDate = DateTime.SpecifyKind(request.ReleaseDate, DateTimeKind.Utc),
                Publisher = request.Publisher,
                Developer = request.Developer,
                Genres = genres,
            };

            entity.Images = request.ImageUrl.Select((path, index) => new GameImageEntity
            {
                ImagePath = path,
                IsMain = index == 0,
                GameId = entity.Id
            }).ToList();

            await _gameRepository.CreateAsync(entity);
            return Ok("Гру успішно додано");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGameRequest request)
        {
            var game = await _gameRepository.GetByIdAsync(request.Id);
            if (game == null)
                return BadRequest($"Гру з ID {request.Id} не знайдено");

            if (!string.IsNullOrEmpty(request.Name) && request.Name != "string")
                game.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description) && request.Description != "string")
                game.Description = request.Description;

            if (request.Price.HasValue && request.Price.Value != 0)
                game.Price = request.Price.Value;

            if (request.ReleaseDate.HasValue && request.ReleaseDate.Value != default)
                game.ReleaseDate = DateTime.SpecifyKind(request.ReleaseDate.Value, DateTimeKind.Utc);

            if (!string.IsNullOrEmpty(request.Publisher) && request.Publisher != "string")
                game.Publisher = request.Publisher;

            if (!string.IsNullOrEmpty(request.Developer) && request.Developer != "string")
                game.Developer = request.Developer;

            if (request.GenreId != null && request.GenreId.Any() && request.GenreId.All(id => id != "string"))
            {
                var genres = new List<GenreEntity>();
                foreach (var genreId in request.GenreId)
                {
                    var genre = await _genreRepository.GetByIdAsync(genreId);
                    if (genre != null)
                        genres.Add(genre);
                }
                game.Genres = genres;
            }

            if (request.ImageUrl != null && request.ImageUrl.Any() && request.ImageUrl.All(url => url != "string"))
            {
                game.Images = request.ImageUrl.Select((path, index) => new GameImageEntity
                {
                    ImagePath = path,
                    IsMain = index == 0,
                    GameId = game.Id
                }).ToList();
            }

            await _gameRepository.UpdateAsync(game);
            return Ok("Гру успішно оновлено");
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return BadRequest($"Гру з ID {id} не знайдено");

            await _gameRepository.DeleteAsync(game);
            return Ok("Гру успішно видалено");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID не може бути порожнім");
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return NotFound($"Гру з ID {id} не знайдено");

            return Ok(game);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            if (await _gameRepository.GetAll().CountAsync() == 0)
                return NotFound("Ігор не знайдено");
            var games = await _gameRepository.GetAll().ToListAsync();
            return Ok(games);
        }

        [HttpGet("genre")]
        public async Task<IActionResult> GetByGenreAsync(string genreId)
        {
            if (string.IsNullOrEmpty(genreId))
                return BadRequest("GenreId не може бути порожнім");
            var games = await _gameRepository.GetByGenreAsync(genreId);
            return Ok(games);
        }
    }
}
