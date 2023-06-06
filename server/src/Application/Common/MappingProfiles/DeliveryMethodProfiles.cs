using Application.DeliveryMethods.Queries.ListDeliveryMethods;
using AutoMapper;
using Domain.Core.DeliveryMethods;
using Domain.Core.Orders.ValueObjects;

namespace Application.Common.MappingProfiles;
public class DeliveryMethodProfiles : Profile
{
    public DeliveryMethodProfiles()
    {
        CreateMap<DeliveryMethod, ListDeliveryMethodsResponse>();
        CreateMap<DeliveryMethod, OrderDeliveryMethod>();
    }
}
