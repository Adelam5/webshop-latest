namespace Domain.Exceptions.User;
public class UserNotFoundException : AppException
{
    public UserNotFoundException()
        : base("User not found")
    {
    }
}
