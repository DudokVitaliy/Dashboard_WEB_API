using Dashboard_WEB_API.BLL.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse> LoginAsync(LoginDto dto);
    }
}
