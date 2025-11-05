using Dashboard_WEB_API.BLL.Dtos.Auth;
using Dashboard_WEB_API.BLL.Services;

namespace PD421_Dashboard_WEB_API.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse> LoginAsync(LoginDto dto);
        Task<ServiceResponse> RegisterAsync(RegisterDto dto);
        Task<ServiceResponse> ConfirmEmailAsync(string userId, string token);
    }
}