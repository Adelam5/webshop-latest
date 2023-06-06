using Application.Carts.Commands.CreateOrUpdate;
using Application.Carts.Queries.GetById;
using AutoMapper;
using Domain.Core.Cart;
using Domain.Core.Cart.Entities;

namespace Application.Common.MappingProfiles;
public class CartProfiles : Profile
{
    public CartProfiles()
    {
        CreateMap<Cart, GetCartResponse>();

        CreateMap<CartItem, CartItemResponse>();

        CreateMap<CreateOrUpdateCartCommand, Cart>();

        CreateMap<CartItemCommand, CartItem>();

    }
}
