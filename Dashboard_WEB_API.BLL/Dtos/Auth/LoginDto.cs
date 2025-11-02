using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Auth
{
    public class LoginDto
    {
        [Required (ErrorMessage = "Поле логін є обов`язковим")]
        public required string Login { get; set; }
        [Required (ErrorMessage = "Поле пароль є обов`язковим")]
        [MinLength(6, ErrorMessage = "Пароль повинен містити мінімум 6 символів")]
        public required string Password { get; set; }
    }
}
