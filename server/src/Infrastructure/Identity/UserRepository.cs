using Application.Common.DTOs;
using Application.Users.Commands.UpdateDetails;
using Application.Users.Queries.GetById;
using Application.Users.Queries.List;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Abstractions.Dtos;
using Domain.Abstractions.Interfaces.Repositories;
using Domain.Exceptions;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;
public sealed class UserRepository : IUserRepository
{
    private readonly UserManager<User> userManager;
    private readonly IdentityDataContext context;
    private readonly IMapper mapper;

    public UserRepository(UserManager<User> userManager, IdentityDataContext context, IMapper mapper)
    {
        this.userManager = userManager;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<List<ListUsersResponse>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context
            .Users
            .ProjectTo<ListUsersResponse>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public async Task<GetUserByIdResponse?> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await context
            .Users
            .Where(x => x.Id == id)
            .ProjectTo<GetUserByIdResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<CreatedUserDto?> GetByIdWithEmailConfirmationToken(string id, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
            return null;

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var userDto = mapper.Map<CreatedUserDto>(user);

        userDto.ConfirmationToken = token;

        return userDto;
    }

    public async Task<UserDto?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await context
            .Users
            .Where(x => x.Email == email)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);


    }

    public async Task<string?> UpdateDetails(UpdateUserDetailsCommand user)
    {
        var existingUser = await userManager.FindByIdAsync(user.Id);

        if (existingUser is null)
            return null;

        existingUser = mapper.Map(user, existingUser);

        var result = await userManager.UpdateAsync(existingUser);

        if (!result.Succeeded)
            throw new AppException(result.Errors.First().Code); // TODO: make specific custom exception

        return existingUser.Id;
    }

    public async Task<string?> Remove(string userId)
    {
        var user = await context.Users.FindAsync(userId);

        if (user is null)
            return null;

        await userManager.DeleteAsync(user);

        return user.Id;
    }
}
