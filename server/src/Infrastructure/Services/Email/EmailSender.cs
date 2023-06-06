using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Infrastructure.Services.Email;
internal sealed class EmailSender : IEmailSender
{
    private const string ClientUrl = "https://webshop.up.railway.app";
    private const string SendResetPasswordEmailTemplateId = "d-79d897f618a04d5a92f3e5e13150f2d1";
    private const string SendConfirmationLinkEmailTemplateId = "d-68a53e2d76094220819e3b25f394e22b";
    private readonly IEmailService emailService;

    public EmailSender(IEmailService emailService)
    {

        this.emailService = emailService;
    }

    public async Task<bool> SendConfirmationLinkEmail(string userId, string toEmail, string name, string code)
    {
        var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callback_url = $"{ClientUrl}/verify-email?token={encoded}&userid={userId}";

        return await emailService.SendEmail(
            toEmail: toEmail,
            templateId: SendConfirmationLinkEmailTemplateId,
            templateData: new
            {
                name,
                confirmation = callback_url
            });
    }

    public async Task<bool> SendResetPasswordEmail(string userId, string toEmail, string code)
    {
        var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callback_url = $"{ClientUrl}/resetpassword?token={encoded}&userid={userId}";

        return await emailService.SendEmail(
            toEmail: toEmail,
            templateId: SendResetPasswordEmailTemplateId,
            templateData: new
            {
                confirmation = callback_url
            });
    }
}
