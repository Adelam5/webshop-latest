using Api.Contracts.Order;
using Application.Orders.Commands.Create;
using AutoMapper;

namespace Api.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrUpdateOrderCommand>();
    }
}
