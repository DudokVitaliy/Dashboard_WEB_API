using Dashboard_WEB_API.BLL.Dtos.Auth;
using Dashboard_WEB_API.BLL.Extensions;
using Microsoft.AspNetCore.Mvc;
using PD421_Dashboard_WEB_API.BLL.Services.Auth;
namespace PD421_Dashboard_WEB_API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.ToActionResult(response);
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmailAsync(string? userId, string? token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var response = await _authService.ConfirmEmailAsync(userId, token);
            return this.ToActionResult(response);
        }
    }
}