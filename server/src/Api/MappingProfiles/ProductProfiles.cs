using Api.Contracts.Product;
using Application.Products.Commands.Create;
using Application.Products.Commands.Update;
using AutoMapper;

namespace Api.MappingProfiles;

public class ProductProfiles : Profile
{
    public ProductProfiles()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
    }
}