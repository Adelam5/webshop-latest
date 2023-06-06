using Domain.Primitives.Result;

namespace Domain.Errors;
public static partial class Errors
{
    public static class User
    {
        public static Error NotFound => new(
           code: "User.NotFound",
           message: "User with speciefied identifier was not found.");

    }
}