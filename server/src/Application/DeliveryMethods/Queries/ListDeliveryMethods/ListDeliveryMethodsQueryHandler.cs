using Application.Common.Interfaces.Messaging;
using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Primitives.Result;

namespace Application.DeliveryMethods.Queries.ListDeliveryMethods;
internal sealed class ListDeliveryMethodsQueryHandler : IQueryHandler<ListDeliveryMethodsQuery, List<ListDeliveryMethodsResponse>>
{
    private readonly IMapper mapper;
    private readonly IDeliveryMethodRepository deliveryMethodRepository;

    public ListDeliveryMethodsQueryHandler(IMapper mapper, IDeliveryMethodRepository deliveryMethodRepository)
    {
        this.mapper = mapper;
        this.deliveryMethodRepository = deliveryMethodRepository;
    }

    public async Task<Result<List<ListDeliveryMethodsResponse>>> Handle(ListDeliveryMethodsQuery request, CancellationToken cancellationToken)
    {
        var deliveryMethods = await deliveryMethodRepository.List(cancellationToken);

        return mapper.Map<List<ListDeliveryMethodsResponse>>(deliveryMethods);
    }
}
