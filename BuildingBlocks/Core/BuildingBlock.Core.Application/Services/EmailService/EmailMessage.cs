using BuildingBlock.Core.Domain.ValueObject.Implementation;

namespace BuildingBlock.Core.Application.Services.EmailService;

public class EmailMessage
{
    public EmailMessage(List<EmailAddress> destinationAddresses, string subject, string body)
    {
        DestinationAddresses = destinationAddresses;
        Subject = subject;
        Body = body;
    }

    public List<EmailAddress> DestinationAddresses { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}