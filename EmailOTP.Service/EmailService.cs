using EmailOTP.Domain.Models;
using EmailOTP.Domain.Service;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailOTP.Service
{
   
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _configuration;

        public EmailService(IOptions<EmailSettings> configuration)
        {
            _configuration = configuration;
            
        }
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("NoReply", _configuration.Value.MailAddress));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync(_configuration.Value.UserName, _configuration.Value.Password); // Use actual credentials
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
