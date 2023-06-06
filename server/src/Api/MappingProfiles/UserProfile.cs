using Api.Contracts.User;
using Application.Users.Commands.UpdateDetails;
using AutoMapper;

namespace Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UpdateUserDetailsRequest, UpdateUserDetailsCommand>();

    }
}
