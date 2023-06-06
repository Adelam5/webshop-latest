using Application.Common.Interfaces.Messaging;

namespace Application.Products.Commands.Update;
public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price) : ICommand<Guid>;
