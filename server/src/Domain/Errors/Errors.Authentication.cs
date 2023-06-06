using Domain.Primitives.Result;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error NotAuthenticated => new(
            code: "Auth.NotAuthenticated",
            message: "User is not authenticated.");

        public static Error InvalidCredentials => new(
            code: "Auth.InvalidCred",
            message: "Invalid credentials.");

        public static Error DuplicateEmail => new(
            code: "Auth.DuplicateEmail",
            message: "Email is already in use.");
    }
}
