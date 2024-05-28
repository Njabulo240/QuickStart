using EmailService.Config;
using EmailService.Contracts;
using EmailService.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService.Services
{
    public class EmailNotification : IEmailNotification
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailNotification(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var link = message.Content;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content)
            };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client
                        .Connect(_emailConfig.SmtpServer,
                        _emailConfig.Port,
                        true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client
                        .Authenticate(_emailConfig.UserName,
                        _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client
                        .ConnectAsync(_emailConfig.SmtpServer,
                        _emailConfig.Port,
                        true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client
                        .AuthenticateAsync(_emailConfig.UserName,
                        _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
