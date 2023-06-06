using Domain.Primitives.Result;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Cart
    {
        public static Error NotFound => new(
            code: "Cart.NotFound",
            message: "Cart with specified identifier was not found.");

        public static Error NotRemoved => new(
            code: "Cart.NotRemoved",
            message: "Cart with specified identifier could not be removed.");
    }
}
