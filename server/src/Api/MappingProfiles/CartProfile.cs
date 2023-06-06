using Api.Contracts.Cart;
using Application.Carts.Commands.AddItem;
using Application.Carts.Commands.CreateOrUpdate;
using Application.Carts.Commands.RemoveItem;
using Application.Carts.Commands.UpdateDeliveryMethod;
using AutoMapper;

namespace Api.MappingProfiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CreateOrUpdateCartRequest, CreateOrUpdateCartCommand>();

        CreateMap<CartItemRequest, CartItemCommand>();

        CreateMap<AddItemRequest, AddItemCommand>();
        CreateMap<RemoveItemRequest, RemoveItemCommand>();
        CreateMap<UpdateDeliveryMethodRequest, UpdateDeliveryMethodCommand>();
    }
}
