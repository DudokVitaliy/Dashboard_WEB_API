using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Game
{
    public class CreateGameDto
    {
        [Required(ErrorMessage = "Поле 'Name' обов`язкове")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public List<string> GenreId { get; set; } = [];

        [Required(ErrorMessage = "Поле 'Main Image' є обов'язковим")]
        public required IFormFile MainImage { get; set; }
        public List<IFormFile> Images { get; set; } = [];
    }
}
