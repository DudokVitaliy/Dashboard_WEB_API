
using Microsoft.Extensions.Options;
using Dashboard_WEB_API.BLL.Settings;
using System.Net;
using System.Net.Mail;

namespace Dashboard_WEB_API.BLL.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value;
            _smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);
            _smtpClient.EnableSsl = true;
            _smtpClient.Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password);
        }

        public async Task SendEmailConfirmMessageAsync(string to, string token, string userId)
        {
            string root = Directory.GetCurrentDirectory();
            string path = Path.Combine(root, "storage", "templates", "confirmEmail.html");
            string html = await File.ReadAllTextAsync(path);

            string url = $"http://localhost:5173/confirmemail?userId={userId}&token={token}";
            html = html.Replace("{{CONFIRM_LINK}}", url);

            await SendHtmlEmailAsync(to, "Підтвердження пошти", html);
        }

        public async Task SendHtmlEmailAsync(IEnumerable<string> to, string subject, string body)
        {
            var message = CreateMessage(to, subject, body);
            message.IsBodyHtml = true;
            await SendEmailAsync(message);

        }

        public async Task SendHtmlEmailAsync(string to, string subject, string body)
        {
            await SendHtmlEmailAsync([to], subject, body);
        }

        public async Task SendTextEmailAsync(IEnumerable<string> to, string subject, string body)
        {
            var message = CreateMessage(to, subject, body);
            message.IsBodyHtml = false;
            await SendEmailAsync(message);
        }

        public async Task SendTextEmailAsync(string to, string subject, string body)
        {
            await SendTextEmailAsync(to, subject, body);
        }

        private MailMessage CreateMessage(IEnumerable<string> to, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.Email ?? "");
            foreach (var item in to)
            {
                message.To.Add(item);
            }
            message.Subject = subject;
            message.Body = body;
            return message;
        }

        private async Task SendEmailAsync(MailMessage message)
        {
            await _smtpClient.SendMailAsync(message);
        }
    }
}