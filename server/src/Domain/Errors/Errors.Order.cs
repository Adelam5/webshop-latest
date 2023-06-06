using Domain.Primitives.Result;

namespace Domain.Errors;
public static partial class Errors
{
    public static class Order
    {
        public static Error NotFound => new(
            code: "Order.NotFound",
            message: "Order with specified identifier was not found.");

        public static Error NotRemoved => new(
            code: "Order.NotRemoved",
            message: "Order with specified identifier could not be removed.");
    }
}

