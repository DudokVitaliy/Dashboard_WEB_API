using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Genre
{
    public class CreateGenreDto
    {
        [Required (ErrorMessage ="Поле Name є обов`язковим")]
        public required string Name { get; set; }
    }
}
