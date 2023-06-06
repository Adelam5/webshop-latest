using Api.Contracts.Customer;
using Application.Customers.Commands.Create;
using AutoMapper;

namespace Api.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateCustomerRequest, CreateCustomerCommand>();

        CreateMap<AddressRequest, AddressCommand>();
    }
}
