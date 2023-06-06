using System.Net;

namespace Domain.Exceptions;
public class DatabaseUpdateException : AppException
{
    public DatabaseUpdateException(string message)
        : base($"Error updating database: {message}", HttpStatusCode.InternalServerError)
    {
    }
}
