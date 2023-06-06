using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Core.Customers.Events;

namespace Application.Customers.Events.EmailConfirmation;
internal sealed class DeleteUserWhenCustomerDeletedDomainEventHandler
    : IDomainEventHandler<CustomerDeletedDomainEvent>
{
    private readonly IUserRepository userRepository;

    public DeleteUserWhenCustomerDeletedDomainEventHandler(
        IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await userRepository.Remove(notification.UserId);
    }
}
