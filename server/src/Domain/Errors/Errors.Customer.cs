using Domain.Primitives.Result;

namespace Domain.Errors;
public static partial class Errors
{
    public static class Customer
    {
        public static Error NotFound => new(
            code: "Customer.NotFound",
            message: "Customer with specified identifier was not found.");
    }
}
