using Domain.Primitives.Result;

namespace Domain.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound => new(
            code: "Product.NotFound",
            message: "Product with specified identifier was not found.");

    }
}
