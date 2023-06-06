namespace Application.Common.Interfaces;
public interface IEmailSender
{
    Task<bool> SendConfirmationLinkEmail(string userId, string toEmail, string name, string code);
    Task<bool> SendResetPasswordEmail(string userId, string toEmail, string code);
}
