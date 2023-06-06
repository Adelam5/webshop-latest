using Application.Common.Interfaces.Messaging;

namespace Application.Products.Commands.Create;
public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price) : ICommand<Guid>;
