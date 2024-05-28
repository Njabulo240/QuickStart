using EmailService.Models;

namespace EmailService.Contracts
{
    public interface IEmailConfirm
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
