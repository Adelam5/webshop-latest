using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories.Commands;
using Domain.Core.Customers;
using Domain.Primitives.Result;

namespace Application.Customers.Commands.Create;
internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly IAuthService authService;
    private readonly ICustomerCommandsRepository customerRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateCustomerCommandHandler(IAuthService authService,
        ICustomerCommandsRepository customerRepository, IUnitOfWork unitOfWork)
    {
        this.authService = authService;
        this.customerRepository = customerRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await authService.Register(request.UserData, cancellationToken);

        if (result.IsFailure) return Result.Failure<Guid>(result.Error);

        var (street, city, state, zipcode) = request.Address;

        var userId = result.Value;

        var newCustomer = Customer.Create(userId, street, city, state, zipcode);

        customerRepository.Add(newCustomer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(newCustomer.Id);
    }
}
