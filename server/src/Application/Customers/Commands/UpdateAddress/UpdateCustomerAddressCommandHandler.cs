using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories.Commands;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Customers.Commands.UpdateAddress;

internal sealed class UpdateCustomerAddressCommandHandler
    : ICommandHandler<UpdateCustomerAddressCommand, Guid>
{
    private readonly ICustomerCommandsRepository customerRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateCustomerAddressCommandHandler(ICustomerCommandsRepository customerRepository, IUnitOfWork unitOfWork)
    {
        this.customerRepository = customerRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result.Failure<Guid>(Errors.Customer.NotFound);

        customer.UpdateAddress(
            request.Street,
            request.City,
            request.State,
            request.Zipcode);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(customer.Id);
    }
}
