using EmailService.Models;

namespace EmailService.Contracts
{
    public interface IEmailNotification
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
