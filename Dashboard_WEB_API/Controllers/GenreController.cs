using Dashboard_WEB_API.DAL.Entities;
using Dashboard_WEB_API.DAL.Repositories.GenreRepositoryes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_WEB_API.Controllers
{
    public class UpdateGenreRequest
    { 
        public required string Id { get; set; }
        public required string Name { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] string name)
        {
            var entity = new GenreEntity{ 
                Name = name,
                NormalizedName = name.ToUpper()
            };
            await _genreRepository.CreateAsync(entity);
            return Ok("Жанр успішно додано");
        }
        [HttpPut]
        public async Task <IActionResult> UpdateAsync([FromBody] UpdateGenreRequest request)
        {
            var entity = await _genreRepository.GetByIdAsync(request.Id);
            if (entity == null) 
            {
                return NotFound($"Жанр {request.Name} не знайдено");
            }
            entity.Name = request.Name;
            entity.NormalizedName = request.Name.ToUpper();
            await _genreRepository.UpdateAsync(entity);
            return Ok("Жанр успішно змінено");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest("Ідентифікатор жанру не може бути порожнім");
            }
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return BadRequest($"Жанр з ідентифікатором {id} не знайдено");
            }
            await _genreRepository.DeleteAsync(entity);
            return Ok("Жанр успішно видалено");
        }
        [HttpGet]
        public async Task<IActionResult> GetById(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Ідентифікатор жанру не може бути порожнім");
            }
            var entity = await _genreRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound($"Жанр з ідентифікатором {id} не знайдено");
            }
            return Ok(entity);
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _genreRepository.GetAll().ToListAsync();
            return Ok(entities);
        }
    }
}
