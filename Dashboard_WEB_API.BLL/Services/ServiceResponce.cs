using System.Net;


namespace Dashboard_WEB_API.BLL.Services
{
    public class ServiceResponce
    {
        public required string Message { get; set; }
        public bool IsSuccess { get; set; } = true;
        public object? Data { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

    }
}
