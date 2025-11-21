using Dashboard_WEB_API.BLL.Services;

namespace Dashboard_WEB_API.Middlewares
{
    public class ExceptionHandleMiddlewares
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddlewares(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string error = ex.InnerException != null 
                    ? ex.InnerException.Message
                    : ex.Message;
                var response = new ServiceResponse
                {
                    IsSuccess = false,
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = error
                };
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
