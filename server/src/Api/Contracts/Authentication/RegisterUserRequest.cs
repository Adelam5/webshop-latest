﻿namespace Api.Contracts.Authentication;

public sealed record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
