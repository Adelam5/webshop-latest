using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories.Queries;
using Domain.Errors;
using Domain.Primitives.Result;

namespace Application.Customers.Queries.GetById;
internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    private readonly ICustomerQueriesRepository customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerQueriesRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }
    public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure<GetCustomerByIdResponse>(Errors.Customer.NotFound);

        return Result.Success(customer);
    }
}
