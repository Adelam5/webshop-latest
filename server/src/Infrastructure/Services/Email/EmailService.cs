using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services.Email;
internal class EmailService : IEmailService
{
    private readonly SendGridSettings sendGridSettings;
    private readonly ILogger<EmailService> logger;

    public EmailService(IOptions<SendGridSettings> options, ILogger<EmailService> logger)
    {
        sendGridSettings = options.Value;
        this.logger = logger;
    }

    public async Task<bool> SendEmail(string toEmail, string subject, string content)
    {
        if (string.IsNullOrEmpty(sendGridSettings.ApiKey))
        {
            throw new Exception("Null SendGridKey");
        }

        var client = new SendGridClient(sendGridSettings.ApiKey);

        var email = new SendGridMessage()
        {
            From = new EmailAddress(sendGridSettings.FromEmail, ""),
            Subject = subject,
            PlainTextContent = content,
            HtmlContent = content
        };

        email.AddTo(new EmailAddress(toEmail));

        email.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(email);

        logger.LogInformation(response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully"
            : $"Failure Email to {toEmail}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendEmail(string toEmail, string templateId, object templateData)
    {
        if (string.IsNullOrEmpty(sendGridSettings.ApiKey))
        {
            throw new Exception("Null SendGridKey");
        }

        var client = new SendGridClient(sendGridSettings.ApiKey);

        var email = new SendGridMessage();

        email.SetFrom(new EmailAddress(sendGridSettings.FromEmail, "WebShop"));

        email.AddTo(new EmailAddress(toEmail));

        email.SetClickTracking(false, false);

        email.SetTemplateId(templateId);

        email.SetTemplateData(templateData);

        var response = await client.SendEmailAsync(email);

        logger.LogInformation(response.IsSuccessStatusCode
            ? $"Email to {toEmail} queued successfully"
            : $"Failure Email to {toEmail}");

        return response.IsSuccessStatusCode;
    }
}
