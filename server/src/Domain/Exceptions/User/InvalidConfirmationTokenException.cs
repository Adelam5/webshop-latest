namespace Domain.Exceptions.User;
public class InvalidConfirmationTokenException : ApplicationException
{
    public InvalidConfirmationTokenException() : base("Confirmation token is not valid")
    {
    }
}
