namespace Domain.Exceptions.User;
public class InvalidUserIdException : AppException
{
    public InvalidUserIdException()
        : base("UserId must be a valid GUID.")
    {
    }
}
