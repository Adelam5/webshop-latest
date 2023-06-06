using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories.Queries;
using AutoMapper;
using Domain.Primitives.Result;

namespace Application.Customers.Queries.List;
internal sealed class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, List<ListCustomerDto>>
{
    private readonly ICustomerQueriesRepository customerRepository;
    private readonly IMapper mapper;

    public GetCustomersQueryHandler(ICustomerQueriesRepository customerRepository, IMapper mapper)
    {
        this.customerRepository = customerRepository;
        this.mapper = mapper;
    }

    public async Task<Result<List<ListCustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetAll(cancellationToken);

        return mapper.Map<List<ListCustomerDto>>(customers);
    }
}
