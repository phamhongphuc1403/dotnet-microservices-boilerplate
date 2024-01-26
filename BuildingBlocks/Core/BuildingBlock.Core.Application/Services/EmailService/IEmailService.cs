namespace BuildingBlock.Core.Application.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}