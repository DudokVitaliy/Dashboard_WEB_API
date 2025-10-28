using Dashboard_WEB_API.BLL.Dtos.Genre;
using Dashboard_WEB_API.BLL.Extensions;
using Dashboard_WEB_API.BLL.Services;
using Dashboard_WEB_API.BLL.Services.Genre;
using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            return this.ToActionResult(response);
        }
        [HttpPut]
        public async Task <IActionResult> UpdateAsync(UpdateGenreDto dto)
        {
            var response = await _genreService.UpdateAsync(dto);
            return this.ToActionResult(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if(string.IsNullOrEmpty(id))
            {
                var validationResponse = new ServiceResponce
                {
                    Message = "Ідентифікатор жанру не може бути порожнім",
                    IsSuccess = false,
                    HttpStatusCode = HttpStatusCode.BadRequest
                };
                return this.ToActionResult(validationResponse);
            }
            var response = await _genreService.DeleteAsync(id);
            return this.ToActionResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var response = await _genreService.GetAllAsync();
                return this.ToActionResult(response);
            }
            else
            {
                var response = await _genreService.GetByIdAsync(id);
                if (response == null)
                {
                    return NotFound($"Жанр з id: {id} не знайдено");
                }
                return this.ToActionResult(response);
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
