using Dashboard_WEB_API.BLL.Dtos.Auth;
using Dashboard_WEB_API.BLL.Extensions;
using Dashboard_WEB_API.BLL.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_WEB_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
