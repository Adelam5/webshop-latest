using Application.Orders.Commands.Create;
using AutoMapper;
using Domain.Core.Common.ValueObjects;
using Domain.Core.OrderAggregate.Entities;
using Domain.Core.Orders;

namespace Application.Common.MappingProfiles;
public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        CreateMap<Order, CreateOrUpdateOrderResponse>()
            .ForCtorParam("DeliveryMethodId", opt => opt.MapFrom(src => src.DeliveryMethod.Id))
            .ForCtorParam("DeliveryMethodPrice", opt => opt.MapFrom(src => src.DeliveryMethod.Price));

        CreateMap<OrderCustomer, CreateOrUpdateOrderCustomer>()
            .ForCtorParam("Address", opt => opt.MapFrom(src => src.DeliveryAddress));

        CreateMap<Address, CreateOrUpdateOrderCustomerAddress>();

        CreateMap<OrderItem, CreateOrUpdateOrderItem>()
            .ForCtorParam("ProductId", opt => opt.MapFrom(dest => dest.Id));
    }
}
