using Azure;
using Azure.Communication.Email;
using EmailService.Models;

namespace EmailService.Services;

public class EmailService : IEmailService
{
   private readonly EmailClient _client;
    private readonly string _sender;

    public EmailService(IConfiguration config)
    {
        var connectionString = config["ACSConnectionString-GroupProject"];
        _sender = config["ACSEmailFromAddress"] ?? throw new Exception("Sender address not configured");


        if (string.IsNullOrEmpty(connectionString))
            throw new Exception("ACS Email connection string not configured");

        _client = new EmailClient(connectionString);

    }

    public async Task<string> SendEmailAsync(SendEmailRequest request)
    {
        var recipients = request.Recipients.Select(r => new EmailAddress(r)).ToList();

        var email = new EmailMessage(senderAddress: _sender,
            content: new EmailContent(request.Subject)
            {
                PlainText = request.TextContent,
                Html = request.HtmlContent
            },
            recipients: new EmailRecipients(recipients)
        );
        var op = await _client.SendAsync(Azure.WaitUntil.Completed, email);
        return op.Value.Status.ToString();
    }
}
