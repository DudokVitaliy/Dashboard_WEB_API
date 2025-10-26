using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Services.Game;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GameRepositores;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dashboard_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameDto dto)
        {
            if (dto == null)
                return BadRequest("Дані гри не можуть бути порожніми");

            var result = await _gameService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGameDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Id))
                return BadRequest("Невірні дані для оновлення");

            var result = await _gameService.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID не може бути порожнім");

            var result = await _gameService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("ID не може бути порожнім");

            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
                return NotFound($"Гру з ID {id} не знайдено");

            return Ok(game);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var games = await _gameService.GetAllAsync();
            if (!games.Any())
                return NotFound("Ігор не знайдено");

            return Ok(games);
        }

        [HttpGet("genre")]
        public async Task<IActionResult> GetByGenreAsync(string genreId)
        {
            if (string.IsNullOrEmpty(genreId))
                return BadRequest("GenreId не може бути порожнім");

            var games = await _gameService.GetGamesByGenreAsync(genreId);
            return Ok(games);
        }
    }
}
