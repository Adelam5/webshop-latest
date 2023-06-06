namespace Application.Users.Queries.GetById;
public sealed record GetUserByIdResponse(string Id, string FirstName, string LastName, string Email, string Roles);
