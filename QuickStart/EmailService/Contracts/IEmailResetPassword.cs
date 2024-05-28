using EmailService.Models;

namespace EmailService.Contracts
{
    public interface IEmailResetPassword
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
