namespace Api.Contracts.Product;

public record UpdateProductRequest(string Name, string Description, decimal Price)
{
    public Guid Id { get; set; }
};

