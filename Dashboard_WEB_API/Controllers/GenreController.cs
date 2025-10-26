using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.BLL.Services.Genre;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var response = await _genreService.CreateAsync(dto);
            return Ok(response);
        }
        [HttpPut]
        public async Task <IActionResult> UpdateAsync(UpdateGenreDto dto)
        {
            var response = await _genreService.UpdateAsync(dto);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest("Ідентифікатор жанру не може бути порожнім");
            }
            var response = await _genreService.DeleteAsync(id);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var genres = await _genreService.GetAllAsync();
                return Ok(genres);
            }
            else
            {
                var genre = await _genreService.GetByIdAsync(id);
                if (genre == null)
                {
                    return NotFound($"Жанр з id: {id} не знайдено");
                }
                return Ok(genre);
            }
             
        }
        //[HttpGet("list")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var genres = await _genreService.GetAllAsync();
        //    return Ok(genres);
        //}
    }
}
