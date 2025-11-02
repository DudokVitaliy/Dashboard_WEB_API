using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Поле 'email' є обов'язковим")]
        [EmailAddress(ErrorMessage = "Невірний формат пошти")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Поле 'userName' є обов'язковим")]
        public required string UserName { get; set; }
        [Required(ErrorMessage = "Поле 'password' є обов'язковим")]
        [MinLength(6, ErrorMessage = "Мінімалльна довжина паролю 6 символів")]
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
