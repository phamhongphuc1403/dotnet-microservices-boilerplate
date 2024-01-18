namespace BuildingBlock.Core.Application.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}