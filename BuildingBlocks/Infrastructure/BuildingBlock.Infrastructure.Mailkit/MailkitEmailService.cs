using BuildingBlock.Core.Application.Services.EmailService;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace BuildingBlock.Infrastructure.Mailkit;

public class MailkitEmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;
    private readonly ILogger<MailkitEmailService> _logger;

    public MailkitEmailService(EmailConfiguration emailConfiguration, ILogger<MailkitEmailService> logger)
    {
        _emailConfiguration = emailConfiguration;
        _logger = logger;
    }

    public async Task SendEmailAsync(EmailMessage emailMessage)
    {
        var mimeMessage = CreateMimeMessage(emailMessage);

        await SendAsync(mimeMessage);
    }

    private MimeMessage CreateMimeMessage(EmailMessage emailMessage)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.Username));

        mimeMessage.To.AddRange(emailMessage.DestinationAddresses.Select(emailAddress =>
            new MailboxAddress(string.Empty, emailAddress.Value)));

        mimeMessage.Subject = emailMessage.Subject;

        mimeMessage.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Body };

        return mimeMessage;
    }

    private async Task SendAsync(MimeMessage mimeMessage)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port, true);

            await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);

            await client.SendAsync(mimeMessage);

            _logger.LogInformation($"Email sent to {mimeMessage.To} susseccfully.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.ToString());

            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);

            client.Dispose();
        }
    }
}