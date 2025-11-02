using Dashboard_WEB_API.DAL.Entities.Identity;

namespace Dashboard_WEB_API.BLL.Services.EmailService
{
    public interface IEmailService
    {
        Task SendTextEmailAsync(IEnumerable<string> to, string subject, string body);
        Task SendTextEmailAsync(string to, string subject, string body);
        Task SendHtmlEmailAsync(IEnumerable<string> to, string subject, string body);
        Task SendHtmlEmailAsync(string to, string subject, string body);
        Task SendEmailConfirmMessageAsync(string to, string token, string userId);
    }
}
