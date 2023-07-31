using Common.Utils.Models;

namespace Services.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(EmailMessage message);
}
