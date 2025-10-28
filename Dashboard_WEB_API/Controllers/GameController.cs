﻿using Dashboard_WEB_API.BLL.Dtos.Game;
using Dashboard_WEB_API.BLL.Extensions;
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

            var response = await _gameService.CreateAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateGameDto dto)
        {
            var response = await _gameService.UpdateAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var response = await _gameService.DeleteAsync(id);
            return this.ToActionResult(response);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var response = await _gameService.GetByIdAsync(id);
            return this.ToActionResult(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _gameService.GetAllAsync();
            return this.ToActionResult(response);
        }

        [HttpGet("genre")]
        public async Task<IActionResult> GetByGenreAsync(string genreId)
        {
            var response = await _gameService.GetGamesByGenreAsync(genreId);
            return this.ToActionResult(response);
        }
    }
}
