namespace Dashboard_WEB_API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Request
            string logMessage = $"Request - {context.Request.Method}: {context.Request.Host}{context.Request.Path}" +
                $"\n\tTime: {DateTime.Now}";
            _logger.LogInformation(logMessage);

            await _next(context);
            // Response
            logMessage = $"Response - {context.Response.StatusCode}: {context.Request.Host}{context.Request.Path}" +
                $"\n\tTime: {DateTime.Now}";
            _logger.LogInformation(logMessage);
        }
    }
}