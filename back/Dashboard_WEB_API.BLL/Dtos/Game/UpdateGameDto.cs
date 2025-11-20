using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Game
{
    public class UpdateGameDto
    {
        [Required (ErrorMessage = "Поле Id обов`язкове")]
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public List<string>? GenreId { get; set; }
        public IFormFile? NewMainImage { get; set; }
        public List<IFormFile>? NewImages { get; set; }
        public List<string>? DeleteImageIds { get; set; }
    }
}
