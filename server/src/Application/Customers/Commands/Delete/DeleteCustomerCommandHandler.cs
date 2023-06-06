using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories.Commands;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Customers.Commands.Delete;
internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Guid>
{
    private readonly ICustomerCommandsRepository customerRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteCustomerCommandHandler(ICustomerCommandsRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        this.customerRepository = customerRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure<Guid>(Errors.Customer.NotFound);

        customerRepository.Remove(customer);

        customer.MarkAsDeleted();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        // Possible problem: customer marked as deleted even if save failes (event published) 

        return Result.Success(customer.Id);
    }
}
