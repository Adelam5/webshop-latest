using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.GetCurrentUser;
using Application.Common.DTOs;
using Application.Users.Commands.UpdateDetails;
using Application.Users.Queries.GetById;
using Application.Users.Queries.List;
using AutoMapper;
using Domain.Abstractions.Dtos;
using Infrastructure.Identity.Entities;

namespace Infrastructure.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForCtorParam("Roles", opt => opt.MapFrom(src =>
                string.Join(", ", src.UserRoles.Select(ur => ur.Role.Name))));
        CreateMap<User, ListUsersResponse>()
            .ForCtorParam("Roles", opt => opt.MapFrom(src =>
                string.Join(", ", src.UserRoles.Select(ur => ur.Role.Name)))); ;
        CreateMap<User, GetUserByIdResponse>()
            .ForCtorParam("Roles", opt => opt.MapFrom(src =>
                string.Join(", ", src.UserRoles.Select(ur => ur.Role.Name)))); ;

        CreateMap<UserDto, User>();

        CreateMap<RegisterUserCommand, User>();

        CreateMap<User, GetCurrentUserResponse>()
            .ForCtorParam("Roles", opt => opt.MapFrom(src =>
                string.Join(", ", src.UserRoles.Select(ur => ur.Role.Name))));

        CreateMap<User, CreatedUserDto>();

        CreateMap<UpdateUserDetailsCommand, User>()
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}
