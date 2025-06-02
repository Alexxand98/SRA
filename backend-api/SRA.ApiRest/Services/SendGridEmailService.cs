using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SRA.ApiRest.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string message)
        {
            var apiKey = _configuration["SendGridSettings:ApiKey"];
            var sender = _configuration["SendGridSettings:FromEmail"];
            var senderName = _configuration["SendGridSettings:FromName"] ?? "Aula AtecA";

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, senderName);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
