namespace Application.Common.Interfaces.Services;
public interface IEmailService
{
    Task<bool> SendEmail(string toEmail, string subject, string content);
    Task<bool> SendEmail(string toEmail, string templateId, object templateData);
}
