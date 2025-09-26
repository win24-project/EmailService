using EmailService.Models;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(SendEmailRequest request);
    }
}
