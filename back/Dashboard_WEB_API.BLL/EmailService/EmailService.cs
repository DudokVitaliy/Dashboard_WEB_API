using Dashboard_WEB_API.BLL.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace PD421_Dashboard_WEB_API.BLL.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpOptions)
        {
            _smtpSettings = smtpOptions.Value ?? throw new ArgumentNullException(nameof(smtpOptions));

            if (string.IsNullOrWhiteSpace(_smtpSettings.Email))
                throw new ArgumentException("SMTP Email is not configured.", nameof(_smtpSettings.Email));

            if (string.IsNullOrWhiteSpace(_smtpSettings.Host))
                throw new ArgumentException("SMTP Host is not configured.", nameof(_smtpSettings.Host));

            _smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpSettings.Email, _smtpSettings.Password)
            };
        }

        public async Task SendEmailConfirmMessageAsync(string to, string token, string userId)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("Recipient email is empty.", nameof(to));

            string root = Directory.GetCurrentDirectory();
            string path = Path.Combine(root, "storage", "templates", "confirmEmail.html");

            if (!File.Exists(path))
                throw new FileNotFoundException($"Email template not found: {path}");

            string html = await File.ReadAllTextAsync(path);

            string url = $"http://localhost:5173/confirmemail?userId={userId}&token={token}";
            html = html.Replace("{{CONFIRM_LINK}}", url);

            await SendHtmlEmailAsync(to, "Підтвердження пошти", html);
        }

        public async Task SendHtmlEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("Recipient email is empty.", nameof(to));

            await SendHtmlEmailAsync(new[] { to }, subject, body);
        }

        public async Task SendHtmlEmailAsync(IEnumerable<string> to, string subject, string body)
        {
            if (to == null || !to.Any() || to.All(string.IsNullOrWhiteSpace))
                throw new ArgumentException("No valid recipient email addresses provided.", nameof(to));

            var message = CreateMessage(to, subject, body, isHtml: true);
            await SendEmailAsync(message);
        }

        public async Task SendTextEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentException("Recipient email is empty.", nameof(to));

            await SendTextEmailAsync(new[] { to }, subject, body);
        }

        public async Task SendTextEmailAsync(IEnumerable<string> to, string subject, string body)
        {
            if (to == null || !to.Any() || to.All(string.IsNullOrWhiteSpace))
                throw new ArgumentException("No valid recipient email addresses provided.", nameof(to));

            var message = CreateMessage(to, subject, body, isHtml: false);
            await SendEmailAsync(message);
        }

        private MailMessage CreateMessage(IEnumerable<string> to, string subject, string body, bool isHtml)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            foreach (var email in to)
            {
                if (!string.IsNullOrWhiteSpace(email))
                    message.To.Add(email);
            }

            if (message.To.Count == 0)
                throw new ArgumentException("No valid recipient email addresses provided.", nameof(to));

            return message;
        }

        private async Task SendEmailAsync(MailMessage message)
        {
            try
            {
                await _smtpClient.SendMailAsync(message);
            }
            catch (SmtpException ex)
            {
                // Можна логувати помилку або повторно кидати виняток
                throw new InvalidOperationException("Помилка при відправці email.", ex);
            }
        }
    }
}
