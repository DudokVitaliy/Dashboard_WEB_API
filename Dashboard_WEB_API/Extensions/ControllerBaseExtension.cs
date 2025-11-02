using Dashboard_WEB_API.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_WEB_API.BLL.Extensions
{
    public static class ControllerBaseExtension
    {
        public static IActionResult ToActionResult(this ControllerBase controller, ServiceResponse response)
        {
            return controller.StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
