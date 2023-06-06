using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Core.Customers.Events;
using Domain.Exceptions.User;

namespace Application.Customers.Events.EmailConfirmation;
internal sealed class SendEmailConfirmationWhenCustomerCreatedDomainEventHandler
    : IDomainEventHandler<CustomerCreatedDomainEvent>
{
    private readonly IUserRepository userRepository;
    private readonly IEmailSender emailSender;

    public SendEmailConfirmationWhenCustomerCreatedDomainEventHandler(
        IUserRepository userRepository,
        IEmailSender emailSender)
    {
        this.userRepository = userRepository;
        this.emailSender = emailSender;
    }

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithEmailConfirmationToken(notification.UserId, cancellationToken)
            ?? throw new UserNotFoundException();

        if (string.IsNullOrEmpty(user.ConfirmationToken))
            throw new InvalidConfirmationTokenException();

        await emailSender.SendConfirmationLinkEmail(
            user.Id,
            user.Email,
            user.FirstName,
            user.ConfirmationToken);
    }
}
