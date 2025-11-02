using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Genre
{
    public class UpdateGenreDto
    {
        [Required(ErrorMessage = "Поле Id є обов`язковим")]
        public required string Id { get; set; }
        [Required(ErrorMessage = "Поле Name є обов`язковим")]
        public required string Name { get; set; }

    }
}
