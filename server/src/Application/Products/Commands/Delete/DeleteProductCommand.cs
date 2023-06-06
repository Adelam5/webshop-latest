using Application.Common.Interfaces.Messaging;

namespace Application.Products.Commands.Delete;
public sealed record DeleteProductCommand(Guid Id) : ICommand<Guid>
{
}
