using Application.Common.DTOs;
using Application.Users.Commands.UpdateDetails;
using Application.Users.Queries.GetById;
using Application.Users.Queries.List;
using Domain.Abstractions.Dtos;

namespace Domain.Abstractions.Interfaces.Repositories;
public interface IUserRepository
{
    Task<List<ListUsersResponse>> GetAll(CancellationToken cancellationToken = default);
    Task<GetUserByIdResponse?> GetById(string Id, CancellationToken cancellationToken = default);
    Task<CreatedUserDto?> GetByIdWithEmailConfirmationToken(
    string Id, CancellationToken cancellationToken = default);
    Task<UserDto?> GetByEmail(string email, CancellationToken cancellationToken = default);
    Task<string?> UpdateDetails(UpdateUserDetailsCommand user);
    Task<string?> Remove(string userId);
}
