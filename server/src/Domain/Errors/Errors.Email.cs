using Domain.Primitives.Result;

namespace Domain.Errors;
public static partial class Errors
{
    public static class Email
    {
        public static Error SendingFailure(string recipientEmail) => new(
            code: "Email.SendingFailure",
            message: $"Failed to send email to {recipientEmail}.");
    }
}
